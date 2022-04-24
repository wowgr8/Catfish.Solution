using System.ComponentModel.DataAnnotations;

namespace CatFish.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  }
}