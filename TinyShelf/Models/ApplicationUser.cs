using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class ApplicationUser : IdentityUser
  {
    public List<Collection> Collections { get; set; }
    public List<Item> Items { get; set; }
  }
}

