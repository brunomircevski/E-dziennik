using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace project.Controllers;

[Authorize(Roles = "Parent")]
public class ParentController : Controller
{
    private readonly IDB DB;
    public ParentController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        List<Student> students = DB.Students
            .Include(x => x.Group)
            .Where(x => x.ParentId == getParentId())
            .ToList();

        if (students.Count == 1) return RedirectToAction("Grades", "Parent", new { id = students.FirstOrDefault().Id });

        ViewBag.students = students;

        return View();
    }

    public IActionResult Grades(int id)
    {
        Student student = DB.Students.Where(x => x.Id == id && x.ParentId == getParentId()).FirstOrDefault();

        if (student is null) return RedirectToAction("AccessDenied", "Account");

        Group group = DB.Students.Where(x => x.Id == id).Select(x => x.Group).FirstOrDefault();

        if (group is null) return RedirectToAction("NotAssigned", "Account");

        List<Subject> subjects = DB.Subjects
            .Include(x => x.Grades.Where(y => y.StudentId == id))
            .Where(x => x.Groups.Contains(group))
            .ToList();

        ViewBag.group = group;
        ViewBag.subjects = subjects;
        ViewBag.student = student;

        return View();
    }

    private int getParentId()
    {
        try
        {
            int id = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            return id;
        }
        catch
        {
            return 0;
        }
    }
}
