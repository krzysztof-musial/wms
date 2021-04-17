using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.UserManagement.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WMS.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [Authorize]
        [HttpGet]
        public IEnumerable<UserLogin> Get()
        {
            return new UserLogin[] { 
                new UserLogin
                {
                    Username = "fdsfds",
                    Password = "cxz"
                },
                new UserLogin
                {
                    Username = "fdsgfdghw2321",
                    Password = "cxzb"
                },
                new UserLogin
                {
                    Username = "dcxvcx",
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
