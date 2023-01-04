using System;
using FactoryProject.Data;
namespace FactoryProject.Models
{
	public class EmployeesBL
	{
		private readonly DataContext _context;
		public EmployeesBL(DataContext context)
		{
			_context = context;
		}


		public IEnumerable<Employees> GetEmployees()
		{
			return _context.Employees.ToList();
		}


		public Employees GetEmployee(int id)
		{
			return _context.Employees.Where(employee => employee.id == id).First();
		}


		public void AddEmployee(Employees newEmployee)
		{
			 _context.Employees.Add(newEmployee);
			_context.SaveChanges();
		}


		public string UpdateEmployee(int id, Employees EmployeeUpdate)
		{
			var oldEmployee = _context.Employees.Where(x => x.id == EmployeeUpdate.id).First();
			oldEmployee.firstName = EmployeeUpdate.firstName;
            oldEmployee.lastName = EmployeeUpdate.lastName;
			oldEmployee.startYear = EmployeeUpdate.startYear;

			_context.SaveChanges();

			return $"Employee {id} Has Been Updated.";
        }

		public string DeleteEmployee(int id)
		{
			var ByeEmployee = _context.Employees.Where(employee => employee.id == id).First();
			_context.Employees.Remove(ByeEmployee);
			_context.SaveChanges();
			return $"Employee [ID ${id} Deleted.";
		}

    }
}

