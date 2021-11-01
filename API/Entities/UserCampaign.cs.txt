namespace API.Entities
{
  public class UserCampaign
  {
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
  }
}