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

        public string LogInUser(string? UserName, string? Password) 
        {
            var CheckUser = _context.Users.Where(user => user.userName == UserName && user.password == Password).First();
            if(CheckUser == null) 
            {
                return "NULL";
            }
            else if(CheckUser != null) 
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
	}
}

