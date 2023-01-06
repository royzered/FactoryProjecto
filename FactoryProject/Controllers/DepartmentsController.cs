using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FactoryProject.Models;

namespace FactoryProject
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentsBL _departmentsBL;

        public DepartmentsController(DepartmentsBL departmentsBL) 
        {
            _departmentsBL = departmentsBL;
        }
        
        // GET: api/Departments
        [HttpGet]
        public IEnumerable<Departments> GetDepartments()
        {
            return _departmentsBL.getDepartments();
        }

        // GET: api/Departments/5
        [HttpGet("{id}", Name = "Get")]
        public Departments GetDepartment(int id)
        {
            return _departmentsBL.getDepartment(id);
        }

        // POST: api/Departments
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
