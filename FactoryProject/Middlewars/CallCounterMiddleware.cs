using System.IdentityModel.Tokens.Jwt;
using FactoryProject.Models;
using Microsoft.AspNetCore.Http;

public class CallCOunterMiddleware 
{
    private readonly RequestDelegate  _next;
    private static int _counter = 60;

    public CallCOunterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        
       if(context.Response.StatusCode == 200)
       {
        _counter--;
       }

    }

    public static int GetCount(int UserActionLeft) 
    {
        int LoggedUserActionLeft = UserActionLeft;
        if(LoggedUserActionLeft > 0)
            {
                LoggedUserActionLeft = _counter;
            }
            else if(LoggedUserActionLeft <= 0)
            {
                LoggedUserActionLeft = 0;
            }
            return  LoggedUserActionLeft;
    }
}