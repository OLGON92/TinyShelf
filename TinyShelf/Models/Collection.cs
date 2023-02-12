using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class Collection
  {
    public int CollectionId { get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; set; }
  }
}