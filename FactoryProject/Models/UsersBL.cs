
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
         private static CurrentUserInfo currentUser = new CurrentUserInfo();

        public UsersBL(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        public int UserActionLimit()
		{
            int maxActions = 60;
            var UserInDb = _context.Users.Where(user => user.id == currentUser.CurrentUserID).First();
            var ActionsLeft = UserInDb.numOfActions;
            ActionCounter =  CallCOunterMiddleware.GetCount(ActionsLeft);
            ActionsLeft = ActionCounter;
            _context.SaveChanges();
            return  ActionsLeft;
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
             currentUser = new CurrentUserInfo
            {
                CurrentUserID = user.id,
                CurrentUserName = user.userName,
                CurrentuserActionsleft = user.numOfActions,
                CurrentReqDate = DateTime.Today.ToShortDateString()

            };
            return currentUser;
        }



        public bool LogOutUser() 
        {
            return true;   
        }
         


	}
}

