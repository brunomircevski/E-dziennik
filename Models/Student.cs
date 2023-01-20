using System.ComponentModel.DataAnnotations;

namespace project.Models;

public class Student : SiteUser
{
    public Student()
    {
        Role = Roles.Student;
    }

    [Required]
    public Parent Parent { get; set; }

    public int ParentId { get; set; }

    public Group Group { get; set; }

    public int? GroupId { get; set; }

    public List<Grade> Grades { get; set; }

}