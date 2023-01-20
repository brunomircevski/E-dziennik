using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace project.Models;

public class Group
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(5)]
    [MinLength(2)]
    [Display(Name = "Klasa")]
    public string Name { get; set; }

    public List<Student> Students { get; set; }

    public Teacher Teacher { get; set; }

    [Required]
    [Display(Name = "Wychowawca")]
    public int TeacherId { get; set; }

    [NotMapped]
    [Display(Name = "Uczniowie w klasie")]
    public List<int> StudentIds { get; set; }

    [Display(Name = "Przedmioty klasy")]
    public List<Subject> Subjects { get; set; }

    [Display(Name = "Wiadomości do klasy")]
    public List<Message> Messages { get; set; }
}