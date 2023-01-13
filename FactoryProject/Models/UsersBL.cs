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

        public bool LogInUser(string UserName, string Password) 
        {
            bool coin = false;
            var CheckUser = _context.Users.Where(user => user.userName == UserName && user.password == Password).First();
            if(CheckUser == null) 
            {
                return coin = false;
            }
            else if(CheckUser != null) 
            {
                return coin = true;
            }
            else
            {
                return coin = false;
            }
        }
	}
}

