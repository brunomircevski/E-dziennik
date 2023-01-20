using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace project.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly IDB DB;
    public StudentController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        int studentId = getStudentId();

        if (studentId == 0) return RedirectToAction("Index", "Home");

        Group group = DB.Students.Where(x=>x.Id == studentId).Select(x => x.Group).FirstOrDefault();

        if( group is null ) return RedirectToAction("NotAssigned", "Account");

        List<Subject> subjects = DB.Subjects
            .Include(x => x.Grades.Where(y => y.StudentId == studentId))
            .Where(x => x.Groups.Contains(group))
            .ToList();

        ViewBag.group = group;
        ViewBag.subjects = subjects;

        return View();
    }

    private int getStudentId()
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
