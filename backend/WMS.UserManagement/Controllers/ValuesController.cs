using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Db;
using WMS.UserManagement.Model.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly WarehouseManagementSystemDataContext _dbContext;

        public ValuesController(IConfiguration configuration, UserManager<User> userManager, WarehouseManagementSystemDataContext dbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        // GET: api/<ValuesController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Login> Get()
        {
            return new Login[] { 
                new Login
                {
                    Email = "fdsfds",
                    Password = "cxz"
                },
                new Login
                {
                    Email = "fdsgfdghw2321",
                    Password = "cxzb"
                },
                new Login
                {
                    Email = "dcxvcx",
                    Password = "3f1"
                }
            };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
