using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FactoryProject.Models;

namespace FactoryProject.Controllers
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
        public IEnumerable<DepartmentsWname> GetDepartments()
        {
            return _departmentsBL.getDepartments();
        }

        // GET: api/Departments/5
        [HttpGet("{id}", Name = "Get")]
        public DepartmentsWname GetDepartment(int id)
        {
            return _departmentsBL.getDepartment(id);
        }

        // POST: api/Departments
        [HttpPost]
        public string Post(Departments NewDepartment)
        {
            _departmentsBL.AddDepartment(NewDepartment);
            return $"Department ID {NewDepartment.id} - {NewDepartment.departmentName} Added.";

        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Departments DepartmentUpdate)
        {
            _departmentsBL.EditDepartment(id, DepartmentUpdate);
            return $"{DepartmentUpdate.departmentName}, ID {id} Has Been Updated.";
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            _departmentsBL.DeleteDepartment(id);
            return $"Department {id} Deleted.";
        }
    }
}
