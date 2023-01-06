using System;
using FactoryProject.Data;

namespace FactoryProject.Models
{
	public class UsersBL
	{
        private readonly DataContext _context;

        public UsersBL(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetUsers()
		{
			return _context.Users.ToList();
		}

        

    
		
	}
}

