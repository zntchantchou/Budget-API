using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
  [Table("Avatars")]
  public class Avatar
  {
    public int AvatarId { get; set; }
    public string Url { get; set; }
    public string PublicId { get; set; }

  /* Fully define relationship to AppUser : 
    - makes appUser non nullable 
    - makes deletion of user cascade to AppUser.Avatar 
  */ 
    
    public int AppUserId { get; set; }
    public AppUser AppUser {get; set;}
  }
}