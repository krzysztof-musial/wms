using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Authentication;
using WMS.UserManagement.Model.Notifications;
using WMS.UserManagement.Model.Common.Response;
using WMS.UserManagement.Model.Warehouse;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using WMS.UserManagement.Model.Services;

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
        private IUserService _userService;

        public UserController(IConfiguration configuration, UserManager<User> userManager, WarehouseManagementSystemDataContext dbContext, IEmailConfiguration emailConfiguration, IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dbContext;
            _emailConfiguration = emailConfiguration;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login userLogin)
        {
            string ipAddress = GetIpAddress();
            var response = await _userService.Login(userLogin, ipAddress);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Registration userRegistration)
        {
            var response = await _userService.Register(userRegistration);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPasswordUser)
        {
            var response = await _userService.ResetPassword(resetPasswordUser);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost]
        [Route("AssignToWarehouse")]
        [Authorize]
        public async Task<ActionResult<SuccessResponse<AssignUserToWarehouse>>> AssignUser([FromBody]AssignUserToWarehouse assignUserToWarehouse)
        {
            var response = await _userService.AssignUserToWarehouse(assignUserToWarehouse, User);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] RefreshToken refreshToken)
        {

            // TO DO add handling access token
            var response = await _userService.RefreshToken(refreshToken, "");

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        private string GetIpAddress()
        {
            if(Request.Headers.ContainsKey("X-Forwarder-For"))
            {
                return Request.Headers["X-Forwarder-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        [Authorize]
        [HttpPost("RevokeToken")]
        public bool RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
