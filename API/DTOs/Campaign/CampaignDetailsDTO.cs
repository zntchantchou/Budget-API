using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
  public class CampaignDetailsDTO
  {
    [Required]
    public string Title { get; set; }

    [StringLength(140, MinimumLength = 2)]
    public string Description { get; set; }

    public ICollection<UserDTO> Users {get; set;}
  }
}