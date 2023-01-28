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
    [Authorize]
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
        public IEnumerable<Shift> GetEmployeeShifts()
        {
            return _shiftsBL.GetShifts();
        }

        // GET: api/Shifts/5
        [HttpGet("{id}", Name = "GetEmployeeshift")]
        public string GetEmployeeShift(int id)
        {
            return "value";
        }

        // POST: api/Shifts
        [HttpPost]
        public string Post(Shift NewShift)
        {
            _shiftsBL.AddShift(NewShift);
            return $"Shift {NewShift.id} Added.";
        }

        // PUT: api/Shifts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
