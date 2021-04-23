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
using WMS.UserManagement.DTO;
using Microsoft.EntityFrameworkCore;

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly WarehouseManagementSystemDataContext _dbContext;

        public UserController(IConfiguration configuration, UserManager<User> userManager, WarehouseManagementSystemDataContext dbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.Email);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userLogin.Password);
                if(passwordCheck)
                {
                    
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));

                    IEnumerable<Claim> claims = new Claim[] { };
                    claims.Append<Claim>(new Claim("userId", user.Id.ToString()));

                    var warehouse = _dbContext.Users.Where(x => x.Id == user.Id).Include(x => x.Warehouse).FirstOrDefault().Warehouse;
                    int? warehouseId = warehouse?.Id;


                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                            { 
                            new Claim("userId", user.Id.ToString()),
                            new Claim("userEmail", user.Email),
                            new Claim("userFirstName", user.FirstName),
                            new Claim("userLastName", user.LastName),
                            new Claim("warehouseId", warehouseId.ToString())
                            }
                        ),
                        SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var stoken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Authentication:ValidIssuer"],
                        audience: _configuration["Authentication:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(stoken)
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
            var user = await _userManager.FindByNameAsync(userRegistration.Email);
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
                Email = userRegistration.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegistration.Email,
                FirstName = userRegistration.FirstName,
                LastName = userRegistration.LastName
            };

            var result = await _userManager.CreateAsync(userToRegister, userRegistration.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = result.Errors.FirstOrDefault().Description });

            return Ok(new Response { Status = "Success", Message = "User created" });
        }
    }
}
