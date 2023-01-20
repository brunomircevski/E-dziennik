using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.DataAccess;

public class MysqlDB : DbContext, IDB
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString: @"Data Source=localhost;port=3306;Initial Catalog=project;User Id=net;password=root",
            new MySqlServerVersion(new Version(10, 6, 7)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().Property(x => x.Salt).HasColumnType("blob");
        modelBuilder.Entity<Student>().Property(x => x.Salt).HasColumnType("blob");
        modelBuilder.Entity<Teacher>().Property(x => x.Salt).HasColumnType("blob");
        modelBuilder.Entity<Parent>().Property(x => x.Salt).HasColumnType("blob");

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Parent)
            .WithMany(p => p.Students)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Group)
            .WithOne(g => g.Teacher)
            .HasForeignKey<Group>(g => g.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Teacher)
            .WithMany(s => s.Subjects)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Teacher)
            .WithMany(t => t.Messages)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Group)
            .WithMany(g => g.Messages)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Announcement>()
            .HasOne(a => a.Teacher)
            .WithMany(t => t.Announcements)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Content>(entity =>
        {
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP()");
        });
    }

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
}
