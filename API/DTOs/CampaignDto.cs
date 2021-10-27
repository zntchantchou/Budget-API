
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
  public class CampaignDto
  {
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Title { get; set; }

    // public ICollection<string> EmailAddresses { get; set; }
    // add currency
  }
}