using System.ComponentModel.DataAnnotations;

namespace project.Models; 

public class LoginUser 
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    [MaxLength(100)]
    public string Password { get; set; }
}