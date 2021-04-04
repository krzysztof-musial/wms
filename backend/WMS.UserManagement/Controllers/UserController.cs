using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WMS.UserManagement.Model;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public UserController(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.Username);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userLogin.Password);
                if(passwordCheck)
                {
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));

                    IEnumerable<Claim> claims = new Claim[] { };
                    claims.Append<Claim>(new Claim("userId", user.Id));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                            { 
                            new Claim("userId", user.Id),
                            new Claim("username", user.UserName)
                            }
                        ),
                        SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var stoken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(stoken),
                        expiration = token.ValidTo,
                        userId = user.Id
                    });
                }
                else
                {
                    return Unauthorized("Wrong password");
                }                
            }
            else
            {
                return Unauthorized("User not found");
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistration userRegistration)
        {
            var user = await _userManager.FindByNameAsync(userRegistration.Username);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User is already registered"
                });
            }
            
            User userToRegister = new User
            {
                Email = userRegistration.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegistration.Username                
            };

            var result = await _userManager.CreateAsync(userToRegister, userRegistration.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = result.Errors.FirstOrDefault().Description });

            return Ok(new Response { Status = "Success", Message = "User created" });
        }
    }
}
