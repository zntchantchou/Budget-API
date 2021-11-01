using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class AppUser
  {
    public int AppUserId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required]
    public bool Active { get; set; } = true;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Avatar Avatar { get; set; }
    public ICollection<Campaign> Campaigns { get; set; }
  }
}
