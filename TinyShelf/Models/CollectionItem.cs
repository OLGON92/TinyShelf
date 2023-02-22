namespace TinyShelf.Models
{
  public class CollectionItem
    {
      public int CollectionItemId { get; set; }
      public int ItemId { get; set; }
      public Item Item { get; set; }
      public int CollectionId { get; set; }
      public Collection Collection { get; set; }
    }
}