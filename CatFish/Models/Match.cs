namespace CatFish.Models
{
  public class Match
  {
    public int MatchId;

    public string UserId;

    public virtual ApplicationUser ApplicationUser { get; set; }
  }
}