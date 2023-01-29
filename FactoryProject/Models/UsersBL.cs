
using System.IdentityModel.Tokens.Jwt;
using FactoryProject.Data;
using System.Web;
using Microsoft.AspNetCore.Mvc;

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

        public int getActions([FromHeader] string token) {
            var TokenHandler = new JwtSecurityTokenHandler();
            var JsonToken  = TokenHandler.ReadToken(token);
            var tokenSec = TokenHandler.ReadToken(token) as JwtSecurityToken;
            var userId =  tokenSec.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
            var userActionNum = _context.Users.Where(user => user.id == Int32.Parse(userId)).First();
            return userActionNum.numOfActions;
        }

        public bool LogOutUser() 
        {
         return true;   
        }
         


	}
}

