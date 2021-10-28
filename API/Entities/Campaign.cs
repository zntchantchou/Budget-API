using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Campaign
  {
    public int Id { get; set; }
    public virtual AppUser Admin { get; set; }
    [Required]
    public int AdminId { get; set; }
    [Required]
    public string Title { get; set; }
    [StringLength(140, MinimumLength = 2)]
    public string Description { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public virtual ICollection<AppUser> Users { get; set; } = new HashSet<AppUser>();
    public virtual ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
  }
}