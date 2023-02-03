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
            _context.IDs.Add(newAssign);
            _context.SaveChanges();
            return newAssign.id;
        }
        
        public List<EmpShift> GetEmpShifts() 
        {
            List<EmpShift>  EmpShiftDetailed = new List<EmpShift>();

            foreach (var item in _context.IDs)
            {
                var shift = _context.Shift.Where(shift => shift.id == item.shiftID).First();
                EmpShift newEmpShift = new EmpShift();
                var emp = _context.Employees.FirstOrDefault(emp => item.employeeID == emp.id);
                if(emp != null)
                { 
                    newEmpShift.employeeID = emp.id;
                    newEmpShift.EmployeeName = $"{emp.firstName} {emp.lastName}";
                }
               
                newEmpShift.id = shift.id;
                newEmpShift.shiftDate = shift.Date.ToShortDateString();
                newEmpShift.startTime = shift.startTime;
                newEmpShift.endTime = shift.endTime;


                EmpShiftDetailed.Add(newEmpShift);
            }

        return  EmpShiftDetailed;
        }   




         public List<EmpShift> ShiftsByEmpId(int empId) 
        {
            List<EmpShift>  EmpShiftDetailed = new List<EmpShift>();

            foreach (var item in _context.IDs)
            {
                var shift = _context.Shift.Where(shift => shift.id == item.shiftID).First();
                EmpShift newEmpShift = new EmpShift();
                var emp = _context.Employees.First(emp => item.employeeID == emp.id);
                if(emp != null)
                { 
                    newEmpShift.employeeID = emp.id;
                    newEmpShift.EmployeeName = $"{emp.firstName} {emp.lastName}";
                }
               
                newEmpShift.id = shift.id;
                newEmpShift.shiftDate = shift.Date.ToShortDateString();
                newEmpShift.startTime = shift.startTime;
                newEmpShift.endTime = shift.endTime;


                EmpShiftDetailed.Add(newEmpShift);
            }

        return  EmpShiftDetailed.Where(shift => shift.employeeID == empId).ToList();
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