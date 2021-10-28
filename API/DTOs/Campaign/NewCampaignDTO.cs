
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
  public class NewCampaignDTO: CampaignDTO
  {
    public string CreatedAt { get; set; }
  }
}