namespace project.Models;

public static class Roles
{
    public static readonly string Student = "Student";
    public static readonly string Parent = "Parent";
    public static readonly string Teacher = "Teacher";
    public static readonly string Admin = "Admin";

    public static string Translate(string role) {
        if(role == Roles.Student) return "Uczeń";
        if(role == Roles.Teacher) return "Nauczyciel";
        if(role == Roles.Parent) return "Rodzic";
        if(role == Roles.Admin) return "Admin";
        return null;
    }
}