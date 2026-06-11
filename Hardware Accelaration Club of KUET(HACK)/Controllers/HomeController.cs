using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hardware_Accelaration_Club_of_KUET_HACK_.Models;

namespace Hardware_Accelaration_Club_of_KUET_HACK_.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Events()
    {
        return View();
    }

      public IActionResult Projects()
    {
        return View();
    }
      public IActionResult Join_Us()
    {
        return View();
    }
      public IActionResult login()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult MyProfile()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
    
}