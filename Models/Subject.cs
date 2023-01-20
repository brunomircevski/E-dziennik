using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace project.Models;

public class Subject
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Przedmiot")]
    public string Name { get; set; }

    [Display(Name = "Nauczyciel")]
    public Teacher Teacher { get; set; }

    [Required]
    [Display(Name = "Nauczyciel")]
    public int TeacherId { get; set; }

    public List<Group> Groups { get; set; }

    [NotMapped]
    [Display(Name = "Klasy z przedmiotem")]
    public List<int> GroupIds { get; set; }

    public List<Grade> Grades { get; set; }

    public List<Content> Contents { get; set; }
}