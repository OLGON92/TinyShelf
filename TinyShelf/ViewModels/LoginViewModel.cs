using System.ComponentModel.DataAnnotations;

namespace TinyShelf.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Username")]
    public string UserName { get; set; }
  }
}