using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Campaign
  {
    public int Id { get; set; }
    [Required]
    public AppUser Admin { get; set; }
    [Required]
    public int AdminId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public IEnumerable<AppUser> Users { get; set; }
  }
}