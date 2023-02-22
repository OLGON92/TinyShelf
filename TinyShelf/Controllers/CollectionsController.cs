using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;

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

    public async Task<ActionResult> Index()
    {
      ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      List<Collection> model = _db.Collections
                      .Where(collection => collection.User.Id == currentUser.Id)
                      .ToList();
      return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }


    [HttpPost]
    public async Task<ActionResult> Create(Collection collection)
    {
      ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      collection.User = currentUser;
      _db.Collections.Add(collection);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> Details(int id)
    {
      ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      Collection thisCollection = _db.Collections
                                      .Include(collection => collection.JoinEntities)
                                      .ThenInclude(join => join.Item)
                                      .FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }

    [Authorize]
    public async Task<ActionResult> Edit(int id)
    {
      ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }


    [HttpPost]
    public async Task<ActionResult> Edit(Collection collection)
    {
      Collection thisCollection = await _db.Collections.FirstOrDefaultAsync(collection => collection.CollectionId == collection.CollectionId);
      var user = await _userManager.GetUserAsync(User);
      if (thisCollection == null || thisCollection.User != user)
      {
        return Unauthorized();
      }
      _db.Collections.Update(collection);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }


    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      Collection thisCollection = await _db.Collections.FirstOrDefaultAsync(collection => collection.CollectionId == collection.CollectionId);
      var user = await _userManager.GetUserAsync(User);
      if (thisCollection == null || thisCollection.User != user)
      {
        return Unauthorized();
      }
      _db.Collections.Remove(thisCollection);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // Adding an AddItem to be able to collect the list for a dropdown collections
    [Authorize]
    public ActionResult AddItem(int id)
    {
      // ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
      // List<Collection> model = _db.Collections
      //                 .Where(collection => collection.User.Id == currentUser.Id)
      //                 .ToList();
      Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
      ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Title");
      return View(thisCollection);
    }

    [HttpPost]
    public async Task<ActionResult> AddItem(Collection collection, int itemId)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
#nullable enable
      CollectionItem? joinEntity = _db.CollectionItems
      .FirstOrDefault(join => (join.ItemId == itemId && join.CollectionId == collection.CollectionId));
#nullable disable
      if (joinEntity == null && itemId != 0)
      {
        _db.CollectionItems.Add(new CollectionItem() { ItemId = itemId, CollectionId = collection.CollectionId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = collection.CollectionId });
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> DeleteJoin(int joinId)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      CollectionItem joinEntry = _db.CollectionItems
                                    .FirstOrDefault(entry => entry.CollectionItemId == joinId);
      _db.CollectionItems.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
