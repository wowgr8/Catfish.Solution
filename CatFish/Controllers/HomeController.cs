using Microsoft.AspNetCore.Mvc;

namespace CatFish.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}