using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FactoryProject.Models
{
	public class OnAction
	{
        private object _context;

        public override void OnActionExecuting(ActionExecutingContext OnAction)
        {
            base.OnActionExecuting(OnAction);
            int tempReq = 0;
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            var userActionNum = _context.Users.Where(user => user.id == Int32.Parse(userId)).First();
            userActionNum.numOfActions--;
            tempReq++;
            if (tempReq == 10)
            {
                _context.SaveChanges();
            }
            else if (tempReq == userActionNum.numOfActions)
            {
                _context.SaveChanges();
            }
        }
    }
}

