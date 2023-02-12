using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class Item
  {
    public int ItemId { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }
  }
}