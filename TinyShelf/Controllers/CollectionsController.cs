using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

using TinyShelf.Models;

namespace TinyShelf.Controllers
{
  public class CollectionsController : Controller
  {
    private readonly TinyShelfContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CollectionsController(UserManager<ApplicationUser> userManager, TinyShelfContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Collection> model = _db.Collections.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Collection collection)
    {
      _db.Collections.Add(collection);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Collection thisCollection = _db.Collections
                                      .Include(collection => collection.Items)
                                      .FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }

    [HttpPost]
    public ActionResult Edit(Collection collection)
    {
        _db.Collections.Update(collection);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
        _db.Collections.Remove(thisCollection);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}
