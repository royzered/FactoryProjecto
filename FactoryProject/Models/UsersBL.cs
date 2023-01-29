
using System.IdentityModel.Tokens.Jwt;
using FactoryProject.Data;
using System.Web;

namespace FactoryProject.Models
{
    public class UsersBL
	{
  
        private readonly DataContext _context;

        private readonly IConfiguration _config;


        public UsersBL(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


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


        public void GetUserActions()
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var Key = _config["Jwt:Key"];
            var Token = HttpContext.Request.Headers["Authorization"];
        }

        public bool LogOutUser() 
        {
         return true;   
        }
         


	}
}

