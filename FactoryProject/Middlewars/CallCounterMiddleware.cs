using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FactoryProject.Models;
using Microsoft.AspNetCore.Http;

public class CallCOunterMiddleware 
{
    private readonly RequestDelegate  _next;
    private static int _counter;
    

    public CallCOunterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
       if(context.Response.StatusCode == 200)
       {
        _counter++;
       }

    }

    public static int GetCount(int UserActionLeft) 
    {
        int LoggedUserActionLeft = UserActionLeft;
        if(LoggedUserActionLeft > 0)
            {
                LoggedUserActionLeft = LoggedUserActionLeft - _counter;
            }
            else
            {
                LoggedUserActionLeft = 0;
            }
            return  LoggedUserActionLeft;
    }
}