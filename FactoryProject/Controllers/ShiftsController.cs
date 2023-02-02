using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FactoryProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace FactoryProject
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {

        private readonly ShiftsBL _shiftsBL;

        public ShiftsController(ShiftsBL shiftsBL) {
            _shiftsBL = shiftsBL;
        }


        // GET: api/Shifts
        [HttpGet]
        public IEnumerable<Shift> GetShifts()
        {
            return _shiftsBL.GetShifts();
        }

         // GET: api/Shifts/EmpShifts/3
        [HttpGet("ShiftDetailed")]
        public IEnumerable<EmpShift> ShiftDetailed(int id)
        {
            return _shiftsBL.GetEmpShifts(id);
        }

        // GET: api/Shifts/5
        [HttpGet("{id}", Name = "GetEmployeeshift")]
        public IEnumerable<EmpShift> GetEmployeeShifts(int id)
        {
         return _shiftsBL.GetEmpShifts(id);

        }

        // POST: api/Shifts
        [HttpPost]
        public ActionResult Post(Shift NewShift)
        {
            _shiftsBL.AddShift(NewShift);
            return Ok("New Shift Added.");
        
        }

        [HttpPost("EmpToShift")]
        public ActionResult EmpToShift([FromBody] IDs empShift)
        {
           var add =  _shiftsBL.EmployeeToShift(empShift);
           if(add > 0)
           {
            return Ok($"{add}, Employee was added to shift successfully.");
           }
           else{
            return BadRequest("Could not assign employee to shift.");
           }
        
        }



        // // PUT: api/Shifts/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE: api/Shifts/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
