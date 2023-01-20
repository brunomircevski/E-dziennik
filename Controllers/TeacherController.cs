using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace project.Controllers;

[Authorize(Roles = "Teacher")]
public class TeacherController : Controller
{
    private readonly IDB DB;
    public TeacherController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Group", "Teacher");
    }

    public IActionResult Group()
    {
        int teacherId = getTeacherId();

        if (teacherId == 0) return RedirectToAction("Index", "Home");

        Group g = DB.Groups
            .Include(x => x.Teacher)
            .Include(x => x.Students)
            .Include(x => x.Subjects)
            .ThenInclude(x => x.Teacher)
            .Where(x => x.Teacher.Id == teacherId)
            .FirstOrDefault();

        if (g is null) return RedirectToAction("AccessDenied", "Account");

        ViewBag.group = g;

        return View();
    }

    public IActionResult Subjects()
    {
        int teacherId = getTeacherId();

        if (teacherId == 0) return RedirectToAction("Index", "Home");

        List<Subject> s = DB.Subjects
            .Include(x => x.Groups)
            .Where(x => x.TeacherId == teacherId)
            .ToList();

        ViewBag.subjects = s;

        return View();
    }

    public IActionResult GroupGrades(int groupId, int subjectId)
    {
        if (groupId == 0 || subjectId == 0) return RedirectToAction("Group", "Teacher");

        Subject s = DB.Subjects
            .Include(x => x.Teacher)
            .Where(x => x.Id == subjectId)
            .FirstOrDefault();

        Group g = DB.Groups
            .Where(x => x.Id == groupId)
            .Include(x => x.Students)
            .ThenInclude(x => x.Grades)
            .FirstOrDefault();

        //ZŁE ROZWIĄZANIE - POWINNO BYĆ W ZAPYTANIU WYŻEJ
        foreach (Student st in g.Students)
        {
            st.Grades = st.Grades.FindAll(x => x.SubjectId == subjectId);
        }

        if (g is null || s is null) return RedirectToAction("Group", "Teacher");

        ViewBag.subject = s;
        ViewBag.group = g;

        return View();
    }

    public IActionResult Subject(int groupId, int subjectId)
    {
        if (groupId == 0 || subjectId == 0) return RedirectToAction("Subjects", "Teacher");

        Subject s = DB.Subjects
            .Include(x => x.Teacher)
            .Where(x => x.Id == subjectId)
            .FirstOrDefault();

        Group g = DB.Groups
            .Where(x => x.Id == groupId)
            .Include(x => x.Students)
            .ThenInclude(x => x.Grades)
            .FirstOrDefault();

        //ZŁE ROZWIĄZANIE - POWINNO BYĆ W ZAPYTANIU WYŻEJ
        foreach (Student st in g.Students)
        {
            st.Grades = st.Grades.FindAll(x => x.SubjectId == subjectId);
        }

        if (g is null || s is null) return RedirectToAction("Group", "Teacher");

        List<SelectListItem> students = g.Students
            .Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name + " (" + a.Email + ")",
            }).ToList();

        List<SelectListItem> grades = Grade.Grades
            .Select(a => new SelectListItem()
            {
                Value = a.Value.ToString(),
                Text = a.DisplayValue,
            }).ToList();

        ViewBag.students = students;
        ViewBag.grades = grades;
        ViewBag.subject = s;
        ViewBag.group = g;

        return View();
    }

    [HttpPost]
    public IActionResult AddGrade(Grade grade)
    {
        if (ModelState.IsValid)
        {   
            grade.Date = DateTime.Now;
            DB.Grades.Add(grade);
            DB.SaveChanges();
        } 

        return RedirectToAction("Subject", "Teacher", new { groupId = grade.GroupId, subjectId = grade.SubjectId });
    }

    private int getTeacherId()
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

    public IActionResult DeleteGrade(int id, int groupId, int subjectId)
    {
        int teacherId = getTeacherId();

        DB.Remove(DB.Grades.Include(x=>x.Subject).Where(x => x.Id == id && x.Subject.TeacherId == teacherId).FirstOrDefault());
        DB.SaveChanges();

        return RedirectToAction("Subject", "Teacher", new { groupId = groupId, subjectId = subjectId });
    }

}
