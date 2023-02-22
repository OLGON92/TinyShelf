using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class Item
  {
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    
    public ApplicationUser User { get; set; }
    public List<CollectionItem> JoinEntities { get; }
  }
}