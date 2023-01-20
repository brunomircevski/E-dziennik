using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models;

public class Grade
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Ocena")]
    [Range(1, 6)]
    public float Value { get; set; }

    [Required]
    [Display(Name = "Waga")]
    [Range(1, 10)]
    public int Weight { get; set; }

    [Display(Name = "Uczeń")]
    public Student Student { get; set; }

    public Subject Subject { get; set; }

    [Required]
    [Display(Name = "Uczeń")]
    public int StudentId { get; set; }

    [Required]
    public int SubjectId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public override string ToString()
    {
        Grade g = Grades.Where(x => x.Value == this.Value).FirstOrDefault();
        if(g is not null) return g.DisplayValue;
        else return null;
    }

    [NotMapped]
    public string DisplayValue { get; set; }

    [NotMapped]
    public int GroupId { get; set; }

    [NotMapped]
    public static Grade[] Grades = {
        new Grade() { Value = 1f,    DisplayValue = "1"},
        new Grade() { Value = 1.25f, DisplayValue = "1+"},
        new Grade() { Value = 1.75f, DisplayValue = "2-"},
        new Grade() { Value = 2f,    DisplayValue = "2"},
        new Grade() { Value = 2.25f, DisplayValue = "2+"},
        new Grade() { Value = 2.75f, DisplayValue = "3-"},
        new Grade() { Value = 3f,    DisplayValue = "3"},
        new Grade() { Value = 3.25f, DisplayValue = "3+"},
        new Grade() { Value = 3.75f, DisplayValue = "4-"},
        new Grade() { Value = 4f,    DisplayValue = "4"},
        new Grade() { Value = 4.25f, DisplayValue = "4+"},
        new Grade() { Value = 4.75f, DisplayValue = "5-"},
        new Grade() { Value = 5f,    DisplayValue = "5"},
        new Grade() { Value = 5.25f, DisplayValue = "5+"},
        new Grade() { Value = 5.75f, DisplayValue = "6-"},
        new Grade() { Value = 6f,    DisplayValue = "6"},
    };
}