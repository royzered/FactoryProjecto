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
            var shifts = _context.Shift.ToList();
            return shifts;
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
        
        public EmpShift GetEmpShifts(int ShiftId) 
        {
            EmpShift DetailedIds = new EmpShift();
            var shift = _context.Shift.Where(shift => shift.id == ShiftId).First();
           
            var IdShift = _context.IDs.Where(id => id.shiftID == shift.id).First();
            EmpShift empShift = new EmpShift();
            empShift.shiftID = IdShift.shiftID;
            empShift.employeeID = IdShift.employeeID;
            var EmployeeName = _context.Employees.Where(emp => emp.id == empShift.employeeID).First();
            empShift.EmployeeName = EmployeeName.firstName + EmployeeName.lastName;
                
            return DetailedIds;
        }

        public void ChangeEmployeeShift(int EmployeeId, int ShiftChange)
        {
            var changeShift = _context.IDs.Where(emp => emp.employeeID == EmployeeId).First();
            changeShift.shiftID = ShiftChange;
            _context.SaveChanges();
        }

        public void DeleteEmployeeShift(int EmployeeId)
        {
            var AssignedShift = _context.IDs.Where(shift => shift.employeeID == EmployeeId).First();
            _context.IDs.Remove(AssignedShift);
            _context.SaveChanges();
        }
    }
}