using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FactoryProject.Data;

namespace FactoryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersBL _usersBL;

        public UsersController(UsersBL usersBL)
        {
            _usersBL = usersBL;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return _usersBL.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
