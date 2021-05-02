using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.DTO;
using Microsoft.EntityFrameworkCore;
using WMS.UserManagement.Model.Authentication;
using WMS.UserManagement.Utils;
using WMS.UserManagement.Model.Notifications;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Warehouse;
using Microsoft.AspNetCore.Authorization;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly WarehouseManagementSystemDataContext _dbContext;
        private readonly IEmailConfiguration _emailConfiguration;

        public UserController(IConfiguration configuration, UserManager<User> userManager, WarehouseManagementSystemDataContext dbContext, IEmailConfiguration emailConfiguration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dbContext;
            _emailConfiguration = emailConfiguration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.Email);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userLogin.Password);
                if(passwordCheck)
                {                    
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));

                    IEnumerable<Claim> claims = new Claim[] { };
                    claims.Append(new Claim("userId", user.Id.ToString()));

                    var warehouse = _dbContext.Users.Where(x => x.Id == user.Id).Include(x => x.Warehouse).FirstOrDefault().Warehouse;
                    int? warehouseId = warehouse?.Id;

                    SecurityTokenDescriptor tokenDescriptor = TokenDescriptor.GetTokenDescriptor(user, warehouse, symmetricSecurityKey);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var stoken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Authentication:ValidIssuer"],
                        audience: _configuration["Authentication:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                    );
                    LoginResult loginResult = new LoginResult
                    {
                        Token = tokenHandler.WriteToken(stoken)
                    };
                    SuccessResponse<LoginResult> response = new SuccessResponse<LoginResult>(loginResult);
                    return Ok(response);
                }
                else
                {
                    FailedResponse response = UserResponse.GetWrongAuthDataResponse();
                    return Unauthorized(response);
                }                
            }
            else
            {
                FailedResponse response = UserResponse.GetWrongAuthDataResponse();
                return Unauthorized(response);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Registration userRegistration)
        {
            bool passwordVerification = userRegistration.WhetherPasswordsAreSame();
            if(!passwordVerification)
            {
                FailedResponse response = UserResponse.GetNotSamePasswordsResponse();
                return BadRequest(response);
            }

            var user = await _userManager.FindByNameAsync(userRegistration.Email);
            if (user != null)
            {
                FailedResponse response = UserResponse.GetUserIsAlreadyRegisteredResponse();
                return BadRequest(response);
            }
            
            User userToRegister = new User
            {
                Email = userRegistration.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegistration.Email,
                FirstName = userRegistration.FirstName,
                LastName = userRegistration.LastName
            };

            var result = await _userManager.CreateAsync(userToRegister, userRegistration.Password);
            if (!result.Succeeded)
            {
                string message = result.Errors.FirstOrDefault() != null ? result.Errors.FirstOrDefault().Description : "Uknown error";
                FailedResponse response = new FailedResponse(message);
                return BadRequest(response);
            }
            var userId = _userManager.FindByEmailAsync(userRegistration.Email).Result.Id;
            var successResponse = UserResponse.GetCreatedUserResponse(userId);
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPasswordUser)
        {
            if (resetPasswordUser?.Email == null)
            {
                FailedResponse response = UserResponse.GetPropertyCannotBeNullResponse("Email");
                return BadRequest(response);
            }

            var user = await _userManager.FindByNameAsync(resetPasswordUser.Email);
            if(user == null)
            {
                FailedResponse response = UserResponse.GetUserNotFoundErrorResponse();
                return BadRequest(response);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            ResetPasswordResult resetPasswordResult = new ResetPasswordResult
            {
                Token = token
            };

            SuccessResponse<ResetPasswordResult> successResponse = new SuccessResponse<ResetPasswordResult>(resetPasswordResult);
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("assigntowarehouse")]
        [Authorize]
        public async Task<ActionResult<SuccessResponse<AssignUserToWarehouse>>> AssignUser([FromBody]AssignUserToWarehouse assignUserToWarehouse)
        {
            User user = await _userManager.FindByIdAsync(assignUserToWarehouse.UserId.ToString());
            string initiatingUserIdAsText = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;
            int initiatingUserId = int.Parse(initiatingUserIdAsText);
            Warehouse warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.UserId == initiatingUserId);
            user.WarehouseId = warehouse.Id;
            var operationResponse = _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            AssignUserToWarehouseResult assignUserToWarehouseResult = new AssignUserToWarehouseResult
            {
                UserId = assignUserToWarehouse.UserId,
                WarehouseId = warehouse.UserId

            };
            SuccessResponse<AssignUserToWarehouseResult> successResponse = new SuccessResponse<AssignUserToWarehouseResult>(assignUserToWarehouseResult);
            return Ok(successResponse);
        }
    }
}
