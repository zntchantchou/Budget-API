using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
  [Table("Avatars")]
  public class Avatar
  {
    public int Id { get; set; }
    public int Url { get; set; }
    public int PublicId { get; set; }

  /* Fully define relationship to AppUser : 
    - makes appUser non nullable 
    - makes deletion of user cascade to AppUser.Avatar 
  */  
    public AppUser AppUser { get; set; }
    public int AppUserId { get; set; }
  }
}