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
        public IEnumerable<Users> GetUsers()
        {
            return _usersBL.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public string GetUser(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public bool PostUser([FromBody] Users LoginTry)
        {
            return _usersBL.LogInUser(LoginTry.userName, LoginTry.password);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void PutUser(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
        }
    }
}
