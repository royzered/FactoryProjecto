
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

        private bool authbool = false;

        public IEnumerable<Users> GetUsers()
		{
			return _context.Users.ToList();
		}
        public Users LogInUser(string? UserName, string? Password) 
        {
            var FindUser = _context.Users.Where(user => user.userName == UserName && user.password == Password).First();
            if(FindUser.numOfActions > 0) 
            {
                return FindUser;
            }
            else
            {
                return null;
            }
        }

        public bool LogoutUser() {
            return authbool = false;
        }



	}
}

