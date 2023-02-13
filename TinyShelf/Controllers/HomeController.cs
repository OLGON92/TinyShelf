using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;

using TinyShelf.Models;

namespace TinyShelf.Controllers
{

  public class HomeController : Controller
  {
      private readonly TinyShelfContext _db;

      private readonly UserManager<ApplicationUser> _userManager;

      public HomeController(UserManager<ApplicationUser> userManager, TinyShelfContext db)
      {
        _userManager = userManager;
        _db = db;
      }

      [HttpGet("/")]

      public ActionResult Index()
      {
        return View();
      }
      /*public IActionResult Index()
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
      }*/
  }
}