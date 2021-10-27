
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
  public class CampaignDTO
  {
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Title { get; set; }

    public List<string> EmailAddresses { get; set; }
    // add currency
  }
}