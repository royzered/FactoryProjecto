
using System.IdentityModel.Tokens.Jwt;
using FactoryProject.Data;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace FactoryProject.Models
{
    public class UsersBL
	{
  
        private readonly DataContext _context;

        private readonly IConfiguration _config;
        
        private static int ActionCounter = 0;

        private static string? LoggedAt;
        private static string? LogoutDate;


         private static Users currentUser = new Users();

        public UsersBL(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public int GetUser() 
        {
            // var TokenHandler = new JwtSecurityTokenHandler();
            // var JsonToken  = TokenHandler.ReadToken(token);
            // var TokenSec = TokenHandler.ReadToken(token) as JwtSecurityToken;
            // var UserId =  TokenSec.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        
            // var UserInDb = _context.Users.Where(user => user.id == Int32.Parse(UserId)).First();
            // return UserInDb;
            return ActionCounter;

        }


        public int UserActionLimit()
		{
            var ActionsLeft = currentUser.numOfActions;
            var current = _context.Users.Where(user => user.id == currentUser.id).First();
            var todayIs = DateTime.Today.Date.ToShortDateString();
            int Checker = 0;
            if(ActionsLeft > 0 && LoggedAt == todayIs)
            {
                 ActionCounter =  CallCOunterMiddleware.GetCount(ActionsLeft);
                ActionsLeft = ActionCounter;
                current.numOfActions = ActionsLeft;
                Checker = current.numOfActions;
                _context.SaveChangesAsync();

            }
           else if(ActionsLeft >= 0 && LoggedAt == todayIs)
           {
            LogOutUser();
            Checker = 0;
           }
           else if(ActionsLeft == 0 && LogoutDate != todayIs || ActionsLeft > 0 && LogoutDate != todayIs)
           {
            int MaxActions = 60;
            current.numOfActions = MaxActions;
            _context.Entry(current).Property("numOfActions").IsModified = true;

                _context.SaveChangesAsync();
           }
           if(current.numOfActions > 0)
           {
            return current.numOfActions;
           }
           else 
           {
            return 0;
           }
		}


        public Users LogInUser(string? UserName, string? Password) 
        {
            var FindUser = _context.Users.Where(user => user.userName == UserName && user.password == Password).First();

            if(FindUser.numOfActions > 0) 
            {
                LoggedAt = DateTime.Today.Date.ToShortDateString();
                currentUser = FindUser;
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
            var CurrentUserLoggedInfo = new CurrentUserInfo
            {
                CurrentUserID = user.id,
                CurrentUserName = user.userName,
                CurrentuserActionsleft = user.numOfActions
            };
            return CurrentUserLoggedInfo;
        }



        public bool LogOutUser() 
        {
            LogoutDate = DateTime.Today.Date.ToShortDateString();
            return true;   
        }
	}
}

