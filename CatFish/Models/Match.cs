namespace CatFish.Models
{
  public class Match
  {
    public int MatchId { get; set; }

    public string UserId { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
  }
}