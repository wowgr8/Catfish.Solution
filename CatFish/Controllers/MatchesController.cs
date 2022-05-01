using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CatFish.Models;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CatFish.Controllers
{
  public class MatchesController : Controller
  {
    private readonly CatFishContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public MatchesController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CatFishContext db, IWebHostEnvironment hostEnvironment)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      var currentUser = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // Grab list of confirmed matches
      List<Match> confirmedMatches = _db.Matches
        .Where(entry => entry.User1Id == currentUser || entry.User2Id == currentUser)
        .Where(entry => entry.User1Response == 1 && entry.User2Response == 1)
        .ToList();
      // Extract match id's for other user
      List<string> matchIds = new List<string>();
      foreach (Match match in confirmedMatches)
      {
        if (match.User1Id == currentUser)
        {
          matchIds.Add(match.User2Id);
        }
        else
        {
          matchIds.Add(match.User1Id);
        }
      }
      // Build list of users to send to view
      var users = _userManager.Users.Where(entry => matchIds.Contains(entry.Id));
      return View(users);
    }

    public ActionResult Browse()
    {
      var currentUser = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // Create matches list from database of all matches and order by if other user has made a match with current user
      List<Match> matches = _db.Matches.ToList();
      List<string> matchedFromOtherUser = new List<string>();
      List<string> completedMatches = new List<string>();
      // Filters users into two lists used to filter final created user list
      foreach (Match match in matches)
      {
        // Adds to matchedFromOtherUser if other user has made a match with the current user before to sort the final list
        if (match.User1Response == 1 && match.User1Id != currentUser && match.User2Id == currentUser)
        {
          matchedFromOtherUser.Add(match.User1Id);
        }
        // Adds to completedMatches which are filtered out of the final user list
        if (match.User1Id == currentUser)
        {
          completedMatches.Add(match.User2Id);
        }
        if (match.User2Id == currentUser && match.User2Response == 1 )
        {
          completedMatches.Add(match.User1Id);
        }
        if (match.User2Id == currentUser && match.User2Response == 0 )
        {
          completedMatches.Add(match.User1Id);
        }
      }
      // Creates final user list and orders them by people who have already matched with current user
      List<ApplicationUser> userList = _userManager.Users
        .Where(entry => !completedMatches.Contains(entry.Id))
        .Where(entry => entry.Id != currentUser)
        .OrderByDescending(entry => matchedFromOtherUser.Contains(entry.Id))
        .ToList();
      if (userList.Count < 1)
      {
        return View(null);
      }
      else
      {
        return View(userList[0]);
      }
    }
    
    [HttpPost]
    public ActionResult Create(int responseValue, string id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      bool result = _db.Matches.Any(entry => entry.User1Id == id && entry.User2Id == userId);

      if(!result)
      {
        Match newMatch = new Match{ User1Id = userId, User2Id = id, User1Response = responseValue, User2Response = 2 };
        _db.Matches.Add(newMatch);
        _db.SaveChanges();
        return RedirectToAction("Browse");
      }
      else
      {
        Match foundMatch = _db.Matches.FirstOrDefault(entry => entry.User1Id == id && entry.User2Id == userId);
        foundMatch.User2Response = responseValue;
        _db.Entry(foundMatch).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Browse");
      }
    }
  }
}
