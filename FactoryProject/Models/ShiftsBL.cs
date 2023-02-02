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

        public   List<Shift> GetShifts() {
            //     List<EmpShift>  EmpShiftDetailed = new List<EmpShift>();

            //     foreach (var id in _context.IDs) 
            //     {
            //         var emp = _context.Employees.Where(emp => emp.id == id.employeeID).First();
            //         var shift = _context.Shift.Where(shift => shift.id == id.shiftID).First();
            //         EmpShift empShiftDits = new EmpShift();
            //         empShiftDits.employeeID = emp.id;
            //         empShiftDits.EmployeeName = $"{emp.firstName} {emp.lastName}";
            //         empShiftDits.id = shift.id;
            //         empShiftDits.shiftDate = shift.Date.ToShortDateString();
            //         empShiftDits.startTime = shift.startTime;
            //         empShiftDits.endTime = shift.endTime;
            //         EmpShiftDetailed.Add(empShiftDits);
            // }       
            // return EmpShiftDetailed.ToList();

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

        public int EmployeeToShift(IDs newAssign)
        {
            // var employee = _context.Employees.Where(employee => employee.id == newAssign.employeeID).First();
            // var shift = _context.Shift.Where(shift => shift.id == newAssign.shiftID).First();
            // IDs NewIds = new IDs();
            // NewIds.employeeID = employee.id;
            // NewIds.shiftID = shift.id;
            _context.IDs.Add(newAssign);
            _context.SaveChanges();
            return newAssign.id;
        }
        
        public IEnumerable<EmpShift> GetEmpShifts(int ShiftId) 
        {
            List<EmpShift>  EmpShiftDetailed = new List<EmpShift>();

            foreach (var id in _context.IDs) 
			{
				var emp = _context.Employees.Where(emp => emp.id == id.employeeID).First();
                var shift = _context.Shift.Where(shift => shift.id == id.shiftID).First();
				 EmpShift empShiftDits = new EmpShift();
                 empShiftDits.employeeID = emp.id;
                 empShiftDits.EmployeeName = $"{emp.firstName} {emp.lastName}";
                 empShiftDits.id = shift.id;
                 empShiftDits.shiftDate = shift.Date.ToShortDateString();
                 empShiftDits.startTime = shift.startTime;
                 empShiftDits.endTime = shift.endTime;
				EmpShiftDetailed.Add(empShiftDits);
        }       
        return EmpShiftDetailed.Where(shift => shift.id == ShiftId).ToList();       
        }            
                                                                                                                                                                                                                                                     
        public void ChangeEmployeeShift(int EmployeeId, int ShiftChange)
        {
            var changeShift = _context.IDs.Where(emp => emp.employeeID == EmployeeId).First();
            changeShift.shiftID = ShiftChange;
            _context.Entry(changeShift).Property("shiftID").IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteEmployeeShift(int EmployeeId)
        {
            var AssignedShift = _context.IDs.Where(shift => shift.employeeID == EmployeeId).ToList();
            _context.IDs.RemoveRange(AssignedShift);
            _context.SaveChanges();
        }
    }
}