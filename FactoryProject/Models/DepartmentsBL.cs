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

        public IEnumerable<Departments> getDepartments() {
			return _context.Departments.ToList();
		}

		public Departments getDepartment(int id) {
			var department = _context.Departments.Where(department => department.id == id).First();
			return department;
		}



    }
}