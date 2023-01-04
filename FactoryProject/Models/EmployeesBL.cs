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

		
	}
}

