using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TinyShelf.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TinyShelf.Controllers
{
  public class ItemsController : Controller
  {
    private readonly TinyShelfContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ItemsController(UserManager<ApplicationUser> userManager, TinyShelfContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Item> model = _db.Items
                            .Include(item => item.Collection)
                            .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Name");
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Item item)
    {
      _db.Items.Add(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items
                          .Include(item => item.Collection)
                          .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Name");
      return View(thisItem);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Edit(Item item)
    {
      Item thisItem = await _db.Items.FirstOrDefaultAsync(item => item.ItemId == item.ItemId);
      var user = await _userManager.GetUserAsync(User);
      if (thisItem == null || thisItem.User != user)
      {
        return Unauthorized();
      }
      _db.Items.Update(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      var user = await _userManager.GetUserAsync(User);
      if (thisItem == null || thisItem.User != user)
      {
        return Unauthorized();
      }
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
