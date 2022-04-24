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
    public class AccountController : Controller
    {
        private readonly CatFishContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CatFishContext db, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            var user = new ApplicationUser 
            { 
                UserName = model.Email, 
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser applicationUser)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currrentUser = await _userManager.FindByIdAsync(userId);
            if (applicationUser.ImageFile != null)
            {
                //TODO Setup Delete function for edit
                // //Delete old image
                // await DeleteImage(applicationUser.applicationUserId);
                //Create new image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(applicationUser.ImageFile.FileName);
                string extention = Path.GetExtension(applicationUser.ImageFile.FileName);
                applicationUser.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssffff") + extention;
                string path = Path.Combine(wwwRootPath + "/img/ProfilePictures/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await applicationUser.ImageFile.CopyToAsync(fileStream);
                }
            }
            currrentUser.FirstName = applicationUser.FirstName;
            currrentUser.LastName = applicationUser.LastName;
            currrentUser.Bio = applicationUser.Bio;
            currrentUser.About = applicationUser.About;
            currrentUser.Age = applicationUser.Age;
            currrentUser.Species = applicationUser.Species;
            currrentUser.Breed = applicationUser.Breed;
            await _userManager.UpdateAsync(currrentUser);
            return RedirectToAction("Index");
        }

        //TODO Refacotor DeleteImage method for user
        
        // public async Task<IActionResult> DeleteImage(int id)
        // {
        //     Treat foundTreat = await _db.Treats.AsNoTracking().FirstOrDefaultAsync(treat => treat.TreatId == id);
        //     var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img/TreatImages/", foundTreat.ImageName);
        //     if (System.IO.File.Exists(imagePath))
        //     {
        //         System.IO.File.Delete(imagePath);
        //     }
        //     return null;
        // }
    }
}