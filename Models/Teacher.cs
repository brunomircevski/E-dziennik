using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace project.Models;

public class Teacher : SiteUser
{
    public Teacher()
    {
        Role = Roles.Teacher;
    }

    public Group Group { get; set; }

    public List<Subject> Subjects { get; set; }

    public List<Message> Messages { get; set; }

    public List<Announcement> Announcements { get; set; }
}