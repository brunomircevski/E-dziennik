using System.ComponentModel.DataAnnotations;

namespace project.Models;

public class Parent : SiteUser
{
    public Parent()
    {
        Role = Roles.Parent;
    }

    public List<Student> Students { get; set; }
}