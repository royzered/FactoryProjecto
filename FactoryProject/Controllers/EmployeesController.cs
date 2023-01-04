using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FactoryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesBL _employeesBL;

        public EmployeesController(EmployeesBL employeesBL)
        {
            _employeesBL = employeesBL;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employees> GetEmployees()
        {
            return _employeesBL.GetEmployees();
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "GetEmployee")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employees
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
