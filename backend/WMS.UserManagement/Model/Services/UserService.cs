using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Authentication;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Warehouse;
using WMS.UserManagement.Utils;

namespace WMS.UserManagement.Model.Services
{
    public class UserService : IUserService
    {
        private WarehouseManagementSystemDataContext _dbContext;
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(WarehouseManagementSystemDataContext dbContext, UserManager<User> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;

        }
        public async Task<IResponse> AssignUserToWarehouse(AssignUserToWarehouse assignUserToWarehouse, ClaimsPrincipal userClaimsPrincipal)
        {
            User user = await _userManager.FindByIdAsync(assignUserToWarehouse.UserId.ToString());
            string initiatingUserIdAsText = userClaimsPrincipal.Claims.FirstOrDefault(x => x.Type == "userId").Value;
            int initiatingUserId = int.Parse(initiatingUserIdAsText);

            bool isUserIsOwnerOfWarehouse = _dbContext.Warehouses.Any(x => x.UserId == assignUserToWarehouse.UserId);
            if (isUserIsOwnerOfWarehouse)
            {
                var response = WarehouseResponse.GetUserIsAlreadyOwnerOfAnotherWarehouse();
                return response;
            }
            Db.Warehouse warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.UserId == initiatingUserId);
            user.WarehouseId = warehouse.Id;
            var operationResponse = _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            AssignUserToWarehouseResult assignUserToWarehouseResult = new AssignUserToWarehouseResult
            {
                UserId = assignUserToWarehouse.UserId,
                WarehouseId = warehouse.UserId

            };
            SuccessResponse<AssignUserToWarehouseResult> successResponse = new SuccessResponse<AssignUserToWarehouseResult>(assignUserToWarehouseResult);
            return successResponse;
        }

        public async Task<IResponse> Login(Login login, string ipAddress)
        {
            var user = await _userManager.FindByNameAsync(login.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));

                    IEnumerable<Claim> claims = new Claim[] { };
                    claims.Append(new Claim("userId", user.Id.ToString()));

                    var warehouse = _dbContext.Users.Where(x => x.Id == user.Id).Include(x => x.Warehouse).FirstOrDefault().Warehouse;
                    int? warehouseId = warehouse?.Id;

                    var jwtToken = GenerateJwtString(user, warehouse);

                    RefreshToken refreshToken = CreateRefreshToken();
                    LoginResult loginResult = new LoginResult
                    {
                        Token = jwtToken,
                        RefreshToken = refreshToken
                    };

                    SuccessResponse<LoginResult> response = new SuccessResponse<LoginResult>(loginResult);
                    return response;
                }
            }

            return UserResponse.GetWrongAuthDataResponse();
        }

        public async Task<IResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var principal = GetUserDetailsFromAccessToken(refreshTokenRequest.AcessToken);
            var username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return UserResponse.GetUserNotFoundErrorResponse();
            }

            RefreshToken refreshToken = user.RefreshToken.FirstOrDefault(x => x.Token == refreshTokenRequest.RefreshToken);

            if(refreshToken == null)
            {
                return TokenResponse.GetRefreshTokenIsNotValidResponse();
            }

            if(refreshToken.IsExpired)
            {
                return TokenResponse.GetRefreshTokenIsExpiredResponse();
            }


            var warehouse = _dbContext.Users.Where(x => x.Id == user.Id).Include(x => x.Warehouse).FirstOrDefault().Warehouse;

            var newRefreshToken = CreateRefreshToken();
            string newAccessToken = GenerateJwtString(user, warehouse);
            var result = new LoginResult
            {
                RefreshToken = newRefreshToken,
                Token = newAccessToken
            };
            SuccessResponse<LoginResult> response = new SuccessResponse<LoginResult>(result);
            return response;
        }

        public async Task<IResponse> Register(Registration registration)
        {
            bool passwordVerification = registration.WhetherPasswordsAreSame();
            if (!passwordVerification)
            {
                FailedResponse response = UserResponse.GetNotSamePasswordsResponse();
                return response;
            }

            var user = await _userManager.FindByNameAsync(registration.Email);
            if (user != null)
            {
                FailedResponse response = UserResponse.GetUserIsAlreadyRegisteredResponse();
                return response;
            }

            User userToRegister = new User
            {
                Email = registration.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registration.Email,
                FirstName = registration.FirstName,
                LastName = registration.LastName
            };

            var result = await _userManager.CreateAsync(userToRegister, registration.Password);

            if (!result.Succeeded)
            {
                string message = result.Errors.FirstOrDefault() != null ? result.Errors.FirstOrDefault().Description : "Uknown error";
                FailedResponse response = new FailedResponse(message);
                return response;
            }

            var userId = _userManager.FindByEmailAsync(registration.Email).Result.Id;
            var successResponse = UserResponse.GetCreatedUserResponse(userId);

            return successResponse;
        }

        public async Task<IResponse> ResetPassword(ResetPassword resetPassword)
        {
            if (resetPassword?.Email == null)
            {
                FailedResponse response = UserResponse.GetPropertyCannotBeNullResponse("Email");
                return response;
            }

            var user = await _userManager.FindByNameAsync(resetPassword.Email);
            if (user == null)
            {
                FailedResponse response = UserResponse.GetUserNotFoundErrorResponse();
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            ResetPasswordResult resetPasswordResult = new ResetPasswordResult
            {
                Token = token
            };

            SuccessResponse<ResetPasswordResult> successResponse = new SuccessResponse<ResetPasswordResult>(resetPasswordResult);
            return successResponse;
        }

        
        public async Task<IResponse> RevokeToken(RevokeTokenRequest revokeTokenRequest)
        {
            var userClaims = GetUserDetailsFromAccessToken(revokeTokenRequest.Token);
            if(userClaims == null)
            {
                var response = TokenResponse.GetAccessTokenIsNotValidResponse();
                return response;
            }

            var user = await _userManager.FindByNameAsync(userClaims.Identity.Name);
            var refreshtoken = user.RefreshToken.FirstOrDefault(x => x.Token == revokeTokenRequest.Token);

            if(refreshtoken != null)
            {
                refreshtoken.Revoked = DateTime.UtcNow;
                _dbContext.Update(refreshtoken);
                _dbContext.SaveChanges();

                RevokeTokenResult revokeTokenResult = new RevokeTokenResult()
                {
                    IsSuccess = true
                };
                var response = new SuccessResponse<RevokeTokenResult>(revokeTokenResult);
                return response;
            }
            else
            {
                var response = TokenResponse.GetRefreshTokenIsNotValidResponse();
                return response;
            }
        }

        private string GenerateJwtString(User user, Db.Warehouse warehouse)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = TokenDescriptor.GetTokenDescriptor(user, warehouse, symmetricSecurityKey);

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:ValidIssuer"],
                audience: _configuration["Authentication:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            return tokenHandler.WriteToken(token);
        }

        private RefreshToken CreateRefreshToken()
        {
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[64];
                cryptoServiceProvider.GetBytes(bytes);
                return new RefreshToken()
                {
                    Token = Convert.ToBase64String(bytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow
                };
            }
        }
        
        private ClaimsPrincipal GetUserDetailsFromAccessToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Authentication:ValidAudience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Authentication:ValidIssuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (securityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
    }
}
