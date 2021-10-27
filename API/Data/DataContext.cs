using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Contributor> Contributors {get; set;}
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AppUser>()
      .HasMany(u => u.Campaigns)
      .WithMany(c => c.Users);

      builder.Entity<Campaign>()
      .HasOne(c => c.Admin);
    }
  }
}
