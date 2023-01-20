using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models; 

public class SiteUser 
{
    [Required]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; }

    [Required]
    public byte[] Salt { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Imie i nazwisko")]
    public string Name { get; set; }

    [NotMapped]
    [Display(Name = "Rola")]
    public string Role { get; set; }
}