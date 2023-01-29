using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FactoryProject.Models;
using FactoryProject.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;


namespace FactoryProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UsersBL _usersBL;
    private readonly DataContext _context;

    public HomeController(ILogger<HomeController> logger, UsersBL usersBL, DataContext context)
    {
        _logger = logger;
        _usersBL = usersBL;
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext OnAction)
    {
        base.OnActionExecuting(OnAction);
        int tempReq = 0;
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
        var userActionNum = _context.Users.Where(user => user.id == Int32.Parse(userId)).First();
        userActionNum.numOfActions--;
        tempReq++;
        if(tempReq == 10)
        {
            _context.SaveChanges();
        }
        else if(tempReq == userActionNum.numOfActions)
        {
            _context.SaveChanges();
        }

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

