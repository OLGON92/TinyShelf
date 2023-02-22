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
      // Collection[] collections = _db.Collections.ToArray();
      // Item[] items = _db.Items.ToArray();
      // Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      // model.Add("collections", collections);
      // model.Add("items", items);
      // return View(model);
      List<Collection> model = _db.Collections
                      .ToList();
      return View(model);

    }

  }
}