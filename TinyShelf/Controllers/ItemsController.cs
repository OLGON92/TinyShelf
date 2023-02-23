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

    public async Task<ActionResult> Index()
    {
      ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      List<Item> model = _db.Items
                      .Where(item => item.User.Id == currentUser.Id)
                      .ToList();
      return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Item item)
    {
      ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      item.User = currentUser;
      _db.Items.Add(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items
                          .Include(item => item.JoinEntities)
                          .ThenInclude(join => join.Collection)
                          .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Name");
      return View(thisItem);
    }


    [HttpPost]
    public async Task<ActionResult> Edit(Item item)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Name");
        return View(item);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        item.User = currentUser;
        _db.Items.Update(item);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public async Task<ActionResult> AddCollection(int id)
    {
      ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      Item thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public async Task<ActionResult> AddCollection(Item item, int collectionId)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
#nullable enable
      CollectionItem? joinEntity = _db.CollectionItems.FirstOrDefault(join => (join.CollectionId == collectionId && join.ItemId == item.ItemId));
#nullable disable
      if (joinEntity == null && collectionId != 0)
      {
        _db.CollectionItems.Add(new CollectionItem() { CollectionId = collectionId, ItemId = item.ItemId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = item.ItemId });
    }

    [HttpPost]
    public async Task<ActionResult> DeleteJoin(int joinId)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      CollectionItem joinEntry = _db.CollectionItems.FirstOrDefault(entry => entry.CollectionItemId == joinId);
      _db.CollectionItems.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
