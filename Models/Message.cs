using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models;

public class Message
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Treść")]
    [MaxLength(5000)]
    public string Content { get; set; }

    [Required]
    public Group Group { get; set; }

    [Required]
    [Display(Name = "Klasa")]
    public int GroupId { get; set; }

    [Required]
    public Teacher Teacher { get; set; }

    [Required]
    [Display(Name = "Nauczyciel")]
    public int TeacherId { get; set; }

    [Required]
    public DateTime Date { get; set; }

}