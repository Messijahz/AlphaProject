using System.ComponentModel.DataAnnotations;

namespace AlphaProject.API.Models;

public class SignInFormModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Email", Prompt = "Enter your email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Password", Prompt = "Enter your password")]
    public string Password { get; set; } = null!;
}
