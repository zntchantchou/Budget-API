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
      var users = await context.Users.ToListAsync();

      var campaigns = new List<Campaign>();
      for (var i = 0; i < 5; i++)
      {
        var newCpgn = new Campaign
        {
          Title = $"Campaign_{i}",
          Description = $"A long description for campaign n°{i}",
          Users = users
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