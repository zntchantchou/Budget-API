using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Campaign
  {
    public int CampaignId { get; set; }
    public AppUser Admin { get; set; }
    [Required]
    public int AdminId { get; set; }
    [Required]
    public string Title { get; set; }
    [StringLength(140, MinimumLength = 2)]
    public string Description { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<UserCampaign> CampaignUsers { get; set; }
    public ICollection<Expense> Expenses { get; set; }
  }
}