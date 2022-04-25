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

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(currentUser);
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

        public async Task<ActionResult> Edit()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser applicationUser)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (applicationUser.ImageFile != null)
            {
                //Delete old image
                if (currentUser.ImageName != null)
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img/ProfilePictures/", currentUser.ImageName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
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
            currentUser.FirstName = applicationUser.FirstName;
            currentUser.LastName = applicationUser.LastName;
            currentUser.Bio = applicationUser.Bio;
            currentUser.About = applicationUser.About;
            currentUser.Age = applicationUser.Age;
            currentUser.Species = applicationUser.Species;
            currentUser.Breed = applicationUser.Breed;
            currentUser.ImageName = applicationUser.ImageName;
            await _userManager.UpdateAsync(currentUser);
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            else
            {
                await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(user);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}