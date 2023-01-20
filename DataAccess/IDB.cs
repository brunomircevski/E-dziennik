using project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace project.DataAccess;

public interface IDB
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Content> Contents { get; set; }


    public int SaveChanges();
    public EntityEntry Remove(object entity);
}
