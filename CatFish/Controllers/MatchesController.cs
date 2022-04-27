using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CatFish.Models;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CatFish.ViewModels;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

    public ActionResult Browse()
    {
        var users = _userManager.Users;
        return View(users);
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