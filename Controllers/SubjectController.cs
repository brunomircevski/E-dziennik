using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace project.Controllers;

[Authorize]
public class SubjectController : Controller
{
    private readonly IDB DB;
    public SubjectController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Add()
    {
        List<SelectListItem> teachers = DB.Teachers
            .Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name + " (" + a.Email + ")",
            }).ToList();

        ViewBag.teachers = teachers;
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Add(Subject subject)
    {
        ViewBag.error = "true";
        if (ModelState.IsValid)
        {
            DB.Subjects.Add(subject);
            DB.SaveChanges();
            ViewBag.error = "false";
        }

        return Add();
    }

    public IActionResult List()
    {
        List<Subject> subjects = DB.Subjects.Include(x => x.Teacher).Include(x => x.Groups).ToList();

        ViewBag.subjects = subjects;
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        Subject s = DB.Subjects.Where(x => x.Id == id).Include(x => x.Groups).FirstOrDefault();

        if (s is null)
        {
            return RedirectToAction("List", "Subject");
        }

        if (s.Groups.Count == 0)
        {
            DB.Remove(s);
            DB.SaveChanges();
        }

        return RedirectToAction("List", "Subject");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Groups(int id)
    {
        Subject s = DB.Subjects.Include(x => x.Groups).Include(x => x.Teacher).Where(x => x.Id == id).FirstOrDefault();

        ViewBag.subject = s;

        List<SelectListItem> groups = DB.Groups
            .Include(x => x.Subjects)
            .Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name + " (" + a.Teacher.Name + ")",
                Selected = (a.Subjects.Contains(s))
            }).ToList();

        ViewBag.groups = groups;

        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Groups(Subject subject)
    {
        Subject s = DB.Subjects.Include(x => x.Groups).Where(x => x.Id == subject.Id).FirstOrDefault();
        if (subject.GroupIds is not null)
            s.Groups = DB.Groups.Where(x => subject.GroupIds.Contains(x.Id)).ToList();
        else
            s.Groups = null;

        DB.SaveChanges();

        return RedirectToAction("List", "Subject");
    }

    public IActionResult Profile(int id)
    {
        Subject s = DB.Subjects.Include(x => x.Teacher).Include(x => x.Groups).Include(x => x.Contents).Where(x => x.Id == id).FirstOrDefault();

        ViewBag.subject = s;
        return View();
    }

    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult Contents(int id)
    {
        Subject s = DB.Subjects.Include(x => x.Contents).Where(x => x.Id == id).FirstOrDefault();

        ViewBag.subject = s;
        return View();
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public IActionResult Contents(Content c)
    {
        ViewBag.error = "true";
        c.Id = 0;

        if (ModelState.IsValid)
        {
            DB.Contents.Add(c);
            DB.SaveChanges();
            ViewBag.error = "false";
        }

        return Contents(c.SubjectId);
    }

    [Authorize(Roles = "Teacher")]
    public IActionResult DeleteContent(int id, int subjectId)
    {
        DB.Remove(DB.Contents.Where(x => x.Id == id).FirstOrDefault());
        DB.SaveChanges();

        return RedirectToAction("Contents", "Subject", new { id = subjectId });
    }
}
