using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvs;
using TinyShelf.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Securtiy.Claims;

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

    // public async Task<ActionResult> Index()
    // {
    //   string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    //   List<Item> userItems = _db.Items
    //                             .Where(entry => entry.User.Id == curentUser.id)
    //                             .Include(item => item.Collection)
    //                             .ToList();
    //   return View(userItems);
    // }

    public ActionResult Create()
    {
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Title");
      return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create(Item item, int CollectionId)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      item.User = currentUser;
      _db.Items.Add(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items
          .Include(item => item.Collection)
          .FirstOfDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOfDefault(item => item.ItemId == id);
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Title");
      return View(thisItem);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit(Item item)
    {
      _db.Items.Update(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Item thisItem = _db.Items.FirstOfDefault(item = > item.ItemId == id);
      return View(thisItem);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Item thisItem = _db.Items.FirstOfDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index")
    }
  }
}
