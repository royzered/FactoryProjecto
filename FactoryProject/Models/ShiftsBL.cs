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

        public List<EmployeeShift> GetShifts() {

            List<EmployeeShift> EmployeeShifts = new List<EmployeeShift>();

            foreach (Employees employee in _context.Employees)
            {
                EmployeeShift EmployeeWShift = new EmployeeShift();
                EmployeeWShift.employeeID = employee.id;
                EmployeeWShift.employeeName = $"{employee.firstName} {employee.lastName}";
                foreach (Shift shift in _context.Shifts)
                {
                    EmployeeWShift.shiftID = shift.id;
                }
                EmployeeShifts.Add(EmployeeWShift);
            }
            return EmployeeShifts;

        }

        public EmployeeShift GetShift(int id)
        {

            EmployeeShift EmployeeShift = new EmployeeShift();
            var shift = _context.Shifts.Where(shift => shift.id == id).First();
            //ahhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh to do
            return EmployeeShift;
            

        }

        public void AddShift(Shift NewShift) 
        {
            _context.Shifts.Add(NewShift);
            _context.SaveChanges();
        }
    }
}