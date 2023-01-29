
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

        public int UserIdFromToken([FromHeader] string token)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var JsonToken  = TokenHandler.ReadToken(token);
            var TokenSec = TokenHandler.ReadToken(token) as JwtSecurityToken;
            var UserId =  TokenSec.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
            return Int32.Parse(UserId);

        }

        public int getActions([FromHeader] string token) {
            int UserId = UserIdFromToken(token);
            var userActionNum = _context.Users.Where(user => user.id == UserId).First();
            return userActionNum.numOfActions;
        }

        public void UserActionsHandler([FromHeader] string token) { //NOTE TO SELF: Should use this function in 'PUT' method as it updates the logged in user's API calls limit (numOfActios)
            int ActionCounter = 0;
            int UserId = UserIdFromToken(token);
            var UserActionsLeft = getActions(token);
            var CurrentUser = _context.Users.Where(user => user.id == UserId).First();

            if(UserActionsLeft > 10) 
            {
                ActionCounter++;
                if(ActionCounter == 10) 
                {
                    CurrentUser.numOfActions = CurrentUser.numOfActions - ActionCounter;
                    _context.SaveChanges();
                    ActionCounter = 0;
                }
                else if (UserActionsLeft < 10)
                {
                    ActionCounter++;
                    if(ActionCounter == 5) 
                    {
                    CurrentUser.numOfActions = CurrentUser.numOfActions - ActionCounter;
                    _context.SaveChanges();
                    ActionCounter = 0;
                        
                    }
                }
            }
        }

        public bool LogOutUser() 
        {
         return true;   
        }
         


	}
}

