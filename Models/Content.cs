using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models;

public class Content
{
    [Required]
    public int Id { get; set; }

    public Subject Subject { get; set; }

    [Required]
    [Display(Name = "Przedmiot")]
    public int SubjectId { get; set; }

    [Display(Name = "Data")]
    public DateTime Date { get; set; }

    [Required]
    [Display(Name = "Nazwa")]
    [MaxLength(100)]
    public String Name { get; set; }

    [Required]
    [Display(Name = "Link")]
    [MaxLength(1000)]
    public String Link { get; set; }
}