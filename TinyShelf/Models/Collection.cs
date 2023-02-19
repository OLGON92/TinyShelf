using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class Collection
  {
    public int CollectionId { get; set; }
    public int ItemId {get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; }
    public ApplicationUser User { get; set; }

  }
}