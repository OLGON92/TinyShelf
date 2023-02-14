using Microsoft.AspNetCore.Identity;

namespace TinyShelf.Models
{
  public class ApplicationUser : IdentityUser
  {

    public ICollection<Item> Items { get; set; }
  }
}

