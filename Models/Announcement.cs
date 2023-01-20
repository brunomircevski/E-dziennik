using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models;

public class Announcement
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Tytuł")]
    [MaxLength(100)]
    [MinLength(3)]
    public string Title { get; set; }

    [Required]
    [Display(Name = "Treść")]
    [MaxLength(5000)]
    public string Content { get; set; }

    public Teacher Teacher { get; set; }

    [Display(Name = "Nauczyciel")]
    public int ?TeacherId { get; set; }

    [Required]
    public DateTime Date { get; set; }

}