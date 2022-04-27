using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CatFish.Models;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CatFish.ViewModels;
using System.Security.Claims;

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

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      return View(currentUser);
    }
  }
}        