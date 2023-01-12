using System;
using FactoryProject.Data;

namespace FactoryProject.Models
{
	public class ShiftsBL
	{
        private readonly DataContext _context;

        public ShiftsBL(DataContext context)
        {
            _context = context;
        }

        public List<Shift> GetShifts() {
            return _context.Shift.ToList();
        }

        public Shift GetShift(int id) 
        {
            return _context.Shift.Where(shift => shift.id == id).First();
        }


        public void AddShift(Shift NewShift) 
        {
            _context.Shift.Add(NewShift);
            _context.SaveChanges();
        }

        public void EmployeeToShift(int EmployeeId, int ShiftId)
        {
            var employee = _context.Employees.Where(employee => employee.id == EmployeeId).First();
            var shift = _context.Shift.Where(shift => shift.id == ShiftId).First();
            IDs NewIds = new IDs();
            NewIds.employeeID = employee.id;
            NewIds.shiftID = shift.id;
            _context.IDs.Add(NewIds);
            _context.SaveChanges();
        }
    }
}