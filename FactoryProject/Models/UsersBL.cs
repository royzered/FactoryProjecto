
using System.IdentityModel.Tokens.Jwt;
using FactoryProject.Data;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

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

        public CurrentUserInfo UserInfoFromToken([FromBody] string token)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var JsonToken  = TokenHandler.ReadToken(token);
            var TokenSec = TokenHandler.ReadToken(token) as JwtSecurityToken;
            var UserId =  TokenSec.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
            var user = _context.Users.Where(user => user.id == Int32.Parse(UserId)).First();
            CurrentUserInfo currentUser = new CurrentUserInfo
            {
                CurrentUserID = user.id,
                CurrentUserName = user.userName,
                CurrentuserActionsleft = user.numOfActions,

            };
        
            return currentUser;
        }


        public void UserActionsHandler([FromHeader] string token) { //NOTE TO SELF: Should use this function in 'PUT' method as it updates the logged in user's API calls limit (numOfActios)
            int ActionCounter = 0;
            int MaxActions = 60;
            CurrentUserInfo currentUser = UserInfoFromToken(token);
            var userInDb = _context.Users.Where(user => user.id == currentUser.CurrentUserID).First();
            int UserActionsLeft = currentUser.CurrentuserActionsleft;

            if (UserActionsLeft > 10) 
            {
                ActionCounter++;
                if(ActionCounter == 10) 
                {
                    userInDb.numOfActions = userInDb.numOfActions - ActionCounter;
                    _context.SaveChanges();
                    ActionCounter = 0;
                }
                else if (UserActionsLeft < 10)
                {
                    ActionCounter++;
                    if(ActionCounter == 5) 
                    {
                     userInDb.numOfActions = userInDb.numOfActions - ActionCounter;
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

