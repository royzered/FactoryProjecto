using System;
using FactoryProject.Data;
namespace FactoryProject.Models
{
	public class DepartmentsBL
	{
		private readonly DataContext _context;
		public DepartmentsBL(DataContext context)
		{
			_context = context;
		}


        public List<DepartmentsWname> getDepartments() {
			List<DepartmentsWname> departments = new List<DepartmentsWname>();

			foreach (Departments deparment in _context.Departments) 
			{
				
				var employees = _context.Employees.Where(employee => employee.id == deparment.manager).First();
				DepartmentsWname newDepartment = new DepartmentsWname();
				newDepartment.id = deparment.id;
				newDepartment.departmentName = deparment.departmentName;
				newDepartment.manager = deparment.manager;
				newDepartment.managerName = $"{employees.firstName} {employees.lastName}";
				departments.Add(newDepartment);
			}
			return departments;
		}

		public DepartmentsWname getDepartment(int id) {
			DepartmentsWname departmentW = new DepartmentsWname();
			var department = _context.Departments.Where(department => department.id == id).First();
			departmentW.id = department.id;
			departmentW.departmentName = department.departmentName;
			departmentW.manager = department.manager;
			var employee = _context.Employees.Where(employee => employee.id == id).First();
			departmentW.managerName = $"{employee.firstName} {employee.lastName}";

			return departmentW;
		}

		public void AddDepartment(Departments newDepartment) 
		{
			_context.Departments.Add(newDepartment);
			var NewManager = _context.Employees.Where(employee => employee.id == newDepartment.manager).First();
			if(NewManager.departmentID != newDepartment.id) {
				NewManager.departmentID = newDepartment.id;
				_context.Entry(NewManager).Property("departmentID").IsModified = true;
				_context.SaveChanges(); 
			}
		}

		public void EditDepartment(int id, Departments DepartmentEdit) 
		{
			var oldDepartment = _context.Departments.Where(department => department.id == id).First();
			oldDepartment.departmentName = DepartmentEdit.departmentName;
			oldDepartment.manager = DepartmentEdit.manager;
			Employees managerUpdate = _context.Employees.Where(emp => emp.id == DepartmentEdit.manager).First();
			if(managerUpdate.departmentID != DepartmentEdit.id)
			 {
				managerUpdate.departmentID = oldDepartment.id;
				_context.Entry(managerUpdate).Property("departmentID").IsModified = true;
				_context.SaveChanges();
			}
		}

		public void DeleteDepartment(int id) 
		{
			var DeleteDepartment = _context.Departments.Where(department => department.id == id).First();
			bool NoEmployeesCheck = true;

			foreach (var employee in _context.Employees)
			{
				if(employee.departmentID != DeleteDepartment.id)
				{
					NoEmployeesCheck = true;
				}

				else
				{
					NoEmployeesCheck = false;
				}
			}

			if(NoEmployeesCheck == true) 
			{
				_context.Remove(DeleteDepartment);
				_context.SaveChanges();
			}
		}
    }
}