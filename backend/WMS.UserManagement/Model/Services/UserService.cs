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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Authentication;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Role;
using WMS.UserManagement.Model.Warehouse;
using WMS.UserManagement.Utils;

namespace WMS.UserManagement.Model.Services
{
    public class UserService : IUserService, IRoleService
    {
        private WarehouseManagementSystemDataContext _dbContext;
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleService _roleService;

        public UserService(WarehouseManagementSystemDataContext dbContext, UserManager<User> userManager, IConfiguration configuration/*, RoleService roleService*/)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
            //_roleService = roleService;

        }
        public async Task<IResponse> AssignUserToWarehouse(AssignUserToWarehouse assignUserToWarehouse, ClaimsPrincipal userClaimsPrincipal)
        {
            User user = await _userManager.FindByIdAsync(assignUserToWarehouse.UserId.ToString());
            string initiatingUserIdAsText = userClaimsPrincipal.Claims.FirstOrDefault(x => x.Type == "userId").Value;
            int initiatingUserId = int.Parse(initiatingUserIdAsText);

            bool isUserIsOwnerOfWarehouse = _dbContext.Warehouses.Any(x => x.UserId == assignUserToWarehouse.UserId);
            if (isUserIsOwnerOfWarehouse)
            {
                var response = WarehouseResponse.GetUserIsAlreadyOwnerOfAnotherWarehouseResponse();
                return response;
            }

            bool isUserAlreadyAssignedToWarehouse = _dbContext.Users.Any(x => x.Id == assignUserToWarehouse.UserId && x.WarehouseId != null);
            if (isUserAlreadyAssignedToWarehouse)
            {
                var response = WarehouseResponse.GetUserIsAlreadyAssignedToAnotherWarehouseResponse();
                return response;
            }
            Db.Warehouse warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.UserId == initiatingUserId);

            if(warehouse == null)
            {
                var response = WarehouseResponse.GetUserIsNotOwnerOfAnyWarehouseResponse();
                return response;
            }

            user.WarehouseId = warehouse.Id;
            var operationResponse = _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            //AssignRoleRequest assignRoleRequest = new AssignRoleRequest()
            //{
            //    UserId = assignUserToWarehouse.UserId,
            //    Role = Model.Common.Enums.RoleType.Worker
            //};
            //var assignRoleResponse = await _roleService.AssignRole(assignRoleRequest);
            //if (!assignRoleResponse.Success)
            //    return assignRoleResponse;

            AssignUserToWarehouseResult assignUserToWarehouseResult = new AssignUserToWarehouseResult
            {
                UserId = assignUserToWarehouse.UserId,
                WarehouseId = (int)warehouse.UserId
            };
            SuccessResponse<AssignUserToWarehouseResult> successResponse = new SuccessResponse<AssignUserToWarehouseResult>(assignUserToWarehouseResult);
            return successResponse;
        }

        public async Task<IResponse> EditUser(int userId, User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException error)
            {
                if (!UserExists(userId))
                {
                    return UserResponse.GetUserNotFoundErrorResponse();
                }
                else
                {
                    var response = new FailedResponse(error.Message);
                }
            }

            var successResponse = new SuccessResponse<User>(null);
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

                    RefreshToken refreshToken = await CreateRefreshToken(user);
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
            var claims = GetUserDetailsFromAccessToken(refreshTokenRequest.AccessToken);
            var userEmail = claims.FirstOrDefault(x => x.Type == "userEmail").Value;

            var user = await _userManager.FindByNameAsync(userEmail);

            if (user == null)
            {
                return UserResponse.GetUserNotFoundErrorResponse();
            }

            RefreshToken refreshToken = _dbContext.RefreshTokens.FirstOrDefault(x => x.Token == refreshTokenRequest.RefreshToken);

            if(refreshToken == null)
            {
                return TokenResponse.GetRefreshTokenIsNotValidResponse();
            }

            if(refreshToken.IsExpired)
            {
                return TokenResponse.GetRefreshTokenIsExpiredResponse();
            }


            var warehouse = _dbContext.Users.Where(x => x.Id == user.Id).Include(x => x.Warehouse).FirstOrDefault().Warehouse;

            var newRefreshToken = await CreateRefreshToken(user);
            string newAccessToken = GenerateJwtString(user, warehouse);
            var result = new LoginResult
            {
                RefreshToken = newRefreshToken,
                Token = newAccessToken
            };
            SuccessResponse<LoginResult> response = new SuccessResponse<LoginResult>(result);
            return response;
        }

        public async Task<IResponse> IsTokenValid(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetTokenValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                var userDetails = GetUserDetailsFromAccessToken(token);

                var claims = GetUserDetailsFromAccessToken(token);
                var userEmail = claims.FirstOrDefault(x => x.Type == "userEmail").Value;

                var user = await _userManager.FindByNameAsync(userEmail);


                TokenValdiationResponse tokenValdiationResponse = new TokenValdiationResponse
                {
                    IsValid = true,
                    User = user
                };

                SuccessResponse<TokenValdiationResponse> successResponse = new SuccessResponse<TokenValdiationResponse>(tokenValdiationResponse);
                return successResponse;
            }
            catch (Exception error)
            {
                var response = TokenResponse.GetAccessTokenIsNotValidResponse(error.Message);
                return response;
            }
            
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

        
        public async Task<IResponse> RevokeToken(RevokeTokenRequest revokeTokenRequest, int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);

            if(user == null)
            {
                var response = TokenResponse.GetAccessTokenIsNotValidResponse();
                return response;
            }

            var refreshtoken = _dbContext.RefreshTokens.FirstOrDefault(x => x.Token == revokeTokenRequest.Token && x.UserId == userId);

            if(refreshtoken != null)
            {
                refreshtoken.Revoked = DateTime.UtcNow;
                _dbContext.Update(refreshtoken);
                await _dbContext.SaveChangesAsync();

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
            var claims = new ClaimsIdentity(new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("userEmail", user.Email),
                new Claim("userFirstName", user.FirstName),
                new Claim("userLastName", user.LastName),
                new Claim("warehouseId", warehouse == null ? "" : warehouse.Id.ToString()),
                new Claim("role", user.Role.ToString())
            });
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.CreateJwtSecurityToken(
                issuer: _configuration["Authentication:ValidIssuer"],
                audience: _configuration["Authentication:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                subject: new ClaimsIdentity(user.Email)
            ) ;
            return tokenHandler.WriteToken(stoken);
        }

        private async Task<RefreshToken> CreateRefreshToken(User user)
        {
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[64];
                cryptoServiceProvider.GetBytes(bytes);
                var refreshToken = new RefreshToken()
                {
                    Token = Convert.ToBase64String(bytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    User = user 
                };
                var t = _dbContext.RefreshTokens.Add(refreshToken);
                var t1 = await _dbContext.SaveChangesAsync();
                return refreshToken;
            }
        }

        public async Task<IResponse> AssignRole(AssignRoleRequest assignRoleRequest)
        {
            if (assignRoleRequest == null)
            {
                FailedResponse response = RoleResponse.GetMissingPropertiesFailedResponse();
                return response;
            }

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == assignRoleRequest.UserId);

            if (user == null)
            {
                FailedResponse response = UserResponse.GetUserNotFoundErrorResponse();
                return response;
            }

            user.Role = assignRoleRequest.Role;
            user.RoleKind = assignRoleRequest.Role.ToString();
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            var assignRoleResponse = new AssignRoleResponse()
            {
                Role = assignRoleRequest.Role,
                UserId = assignRoleRequest.UserId
            };
            var successResponse = new SuccessResponse<AssignRoleResponse>(assignRoleResponse);
            return successResponse;
        }

        private IEnumerable<Claim> GetUserDetailsFromAccessToken(string token)
        {
            var tokenValidationParameters = GetTokenValidationParameters();

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (securityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal.Claims;
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]))
            };
            return tokenValidationParameters;
        }

        private bool UserExists(int id)
        {
            return _dbContext.Users.Any(e => e.Id == id);
        }
    }
}
