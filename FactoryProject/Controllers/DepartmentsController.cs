using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FactoryProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace FactoryProject.Controllers
{
    [Authorize]
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
            return $" {NewDepartment.departmentName} ID {NewDepartment.id}, Added.";

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
        public ActionResult Delete(int id)
        {
            var DepartmentDeleted = _departmentsBL.DeleteDepartment(id);
            if(DepartmentDeleted == true) 
            {
                return Ok($"Department {id} Has Been Deleted.");
            }
            else 
            {
                return BadRequest($"Could Not Delete Department {id}, there are employees in this departemnt");
            }
        }
    }
}
