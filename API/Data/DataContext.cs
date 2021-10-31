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
    public DbSet<UserCampaign> UserCampaigns {get; set;}
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AppUser>()
      .HasIndex(p => p.Email).IsUnique();

      builder.Entity<AppUser>()
      .HasOne(u => u.Avatar)
      .WithOne(a => a.AppUser)
      .HasForeignKey<Avatar>(u => u.AppUserId);

      builder.Entity<UserCampaign>()
      .HasKey(uc => new {uc.UserId, uc.CampaignId});

      builder.Entity<UserCampaign>()
      .HasOne(uc => uc.User)
      .WithMany(u => u.UserCampaigns)
      .HasForeignKey(uc => uc.CampaignId);
      // .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<UserCampaign>()
      .HasOne(uc => uc.Campaign)
      .WithMany(c => c.CampaignUsers)
      .HasForeignKey(uc => uc.UserId);
      // .OnDelete(DeleteBehavior.Cascade);



      // builder.Entity<Campaign>()
      // .HasOne(c => c.Admin)
      // .WithMany(a => a.Campaigns)
      // .HasForeignKey(c => c.AdminId);
    }
  }
}
