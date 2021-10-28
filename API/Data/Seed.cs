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
    public static async Task SeedUserCampaigns(DataContext context)
    {
      Console.WriteLine("Seeding Campaigns...");
      var users = await context.Users.ToListAsync();
      foreach (var user in users)
      {
        Console.WriteLine($"Seeding campaings for user {user.Email}");
        var userCampaigns = new HashSet<Campaign>();
        for (int i = 0; i < 3; i++)
        {
					var otherUsers = users.FindAll(u => u.Email != user.Email);
          var cpgn = new Campaign { 
						Title = $"Campaign_{user.Username}_{i}",
						Description = $"Description for campaign number {i} of user {user.Email}",	
            AdminId = user.Id,
            Users = otherUsers,
					};
          userCampaigns.Add(cpgn);
          user.Campaigns = userCampaigns;
        }
        await context.SaveChangesAsync();
        Console.WriteLine($"Saved campaigns for user {user.Email}");
      }
    }
  }
}