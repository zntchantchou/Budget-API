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

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AppUser>()
      .HasMany<Campaign>(u => u.Campaigns);

      builder.Entity<Campaign>()
      .HasOne<AppUser>(c => c.Admin);

      builder.Entity<Campaign>()
      .HasMany<AppUser>(c => c.Users);
    }
  }
}
