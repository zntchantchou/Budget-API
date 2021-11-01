using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using API.Entities;
using System.Security.Cryptography;
using System.Text;
using System;

namespace API.Data
{
  public class Seed
  {
    public static async Task SeedUsers(DataContext context)
    {
      if (await context.Users.AnyAsync()) return;
      var userData = await System.IO.File.ReadAllTextAsync("Data/userSeedData.json");
      var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
      foreach (var user in users)
      {
        using var hmac = new HMACSHA512();
        Console.WriteLine("Seeding user", user);
        user.Username = user.Username.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$$w0rd"));
        user.PasswordSalt = hmac.Key;
        context.Add(user);
      }
      await context.SaveChangesAsync();
    }
    public static async Task<int> SeedUserCampaigns(DataContext context)
    {
      Console.WriteLine("Seeding Campaigns...");
      // Create Campaigns With every user users
      if(await context.Campaigns.AnyAsync())
     {
       Console.WriteLine("[SeedUserCampaigns] Not seeding as there are already campaigns in the database");
       return 0;
     }
      var users = await context.Users.ToListAsync();

      var campaigns = new List<Campaign>();
      for (var i = 1; i < 6; i++)
      {
        var adminUser = users.Find(u => u.AppUserId == i);
        if(adminUser == null) {
          Console.WriteLine($"[SeedUserCampaigns] Admin user with id {i} not found ");
          return 0;
        } 
        var newCpgn = new Campaign
        {
          Title = $"Campaign_{i}",
          Description = $"A long description for campaign n°{i}",
          Users = users, 
          Admin = adminUser
        };
        campaigns.Add(newCpgn);
      }
      context.Campaigns.AddRange(campaigns);
      var saveResult = await context.SaveChangesAsync();
      Console.WriteLine("Done Seeding Campaings");
      return saveResult; 
    }
  }
}