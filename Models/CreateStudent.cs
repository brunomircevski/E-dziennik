using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace project.Models; 

public class CreateStudent
{
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MaxLength(100)]
    [MinLength(8)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(5)]
    [Display(Name = "Imie i nazwisko")]
    public string Name { get; set; }

    [Display(Name = "Rodzic")]
    public int ParentId { get; set; }
}