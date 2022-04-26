namespace CatFish.Models
{
  public class Match
  {
    public int MatchId { get; set; }

    public string User1Id { get; set; }
    public string User2Id { get; set; }
    public bool User1Response { get; set; }
    public bool User2Response { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
  }
}