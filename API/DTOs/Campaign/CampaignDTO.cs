
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
  public class CampaignDTO
  {
    [Required]
    public string Title { get; set; }
    public List<string> UserEmails { get; set; }
    
    [StringLength(140, MinimumLength = 2)]
    public string Description { get; set; }
  }
}