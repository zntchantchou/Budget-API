
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
  public class UserCampaignDTO
  {
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }
    
  }
}