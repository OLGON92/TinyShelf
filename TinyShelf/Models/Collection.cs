using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class Collection
  {
    public int CollectionId { get; set; }
    public string Name { get; set; }
    public List<Item> Item { get; }
    public ApplicationUser User { get; set; }

  }
}