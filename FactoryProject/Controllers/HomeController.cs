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

