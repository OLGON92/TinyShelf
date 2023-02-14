using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TinyShelf.Models
{
  public class TinyShelfContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Item> Items { get; set; }

    public TinyShelfContext(DbContextOptions options) : base(options) { }
  }
}
