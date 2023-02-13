using System.Collections.Generic;

namespace TinyShelf.Models
{
  public class Item
  {
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
  }
}