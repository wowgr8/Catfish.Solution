using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatFish.Models
{
  public class CatFishContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Match> Matches { get; set; }
    public CatFishContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}