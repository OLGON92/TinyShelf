using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class Collection
  {
    public int CollectionId { get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

  }
}