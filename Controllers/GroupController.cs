using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace project.Controllers;

[Authorize]
public class GroupController : Controller
{
    private readonly IDB DB;
    public GroupController(IDB _DB)
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
            .Include(x => x.Group)
            .Where(x => x.Group == null)
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
    public IActionResult Add(Group group)
    {
        if (ModelState.IsValid)
        {
            if (DB.Groups.Where(x => x.TeacherId == group.TeacherId).FirstOrDefault() is null)
            {
                ViewBag.error = "false";
                DB.Groups.Add(group);
                DB.SaveChanges();
                return Add();
            }
        }
        ViewBag.error = "true";

        return Add();
    }
    public IActionResult List()
    {
        List<Group> groups = DB.Groups.Include(x => x.Teacher).Include(x => x.Students).ToList();

        ViewBag.groups = groups;
        ViewBag.admin = User.IsInRole("Admin");

        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        if (DB.Students.Include(x => x.Group).Where(x => x.Group.Id == id).FirstOrDefault() is null)
        {
            DB.Remove(DB.Groups.Where(x => x.Id == id).FirstOrDefault());
            DB.SaveChanges();
        }

        return RedirectToAction("List", "Group");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        Group group = DB.Groups.Include(x => x.Teacher).Include(x => x.Students).Where(x => x.Id == id).FirstOrDefault();

        List<SelectListItem> teachers = DB.Teachers
            .Include(x => x.Group)
            .Where(x => x.Group == null || x.Group.Id == id)
            .Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name + " (" + a.Email + ")",
                Selected = (a.Id == group.TeacherId)
            }).ToList();

        List<SelectListItem> students = DB.Students
            .Include(x => x.Group)
            .Where(x => x.Group == null || x.Group.Id == id)
            .Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name + " (" + a.Email + ")",
                Selected = (a.Group.Id == id)
            }).ToList();;

        ViewBag.teachers = teachers;
        ViewBag.students = students;
        ViewBag.group = group;

        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Edit(Group group)
    {
        if(group.TeacherId == 0) return RedirectToAction("List", "Group");
        
        Group g = DB.Groups.Where(x => x.Id == group.Id).FirstOrDefault();
        g.Name = group.Name;
        g.TeacherId = group.TeacherId;
        DB.SaveChanges();

        return RedirectToAction("List", "Group");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Students(Group group)
    {
        Group g = DB.Groups.Include(x => x.Students).Where(x => x.Id == group.Id).FirstOrDefault();
        if(group.StudentIds is not null)
            g.Students = DB.Students.Where( x => group.StudentIds.Contains(x.Id)).ToList();
        else
            g.Students = null;
        DB.SaveChanges();

        return RedirectToAction("List", "Group");
    }

    public IActionResult Profile(int id)
    {
        if(id == 0) return RedirectToAction("List", "Group");

        Group g = DB.Groups
            .Include(x => x.Students)
            .Include(x => x.Teacher)
            .Include(x => x.Subjects)
            .ThenInclude(x => x.Teacher)
            .Where(x => x.Id == id)
            .FirstOrDefault();

        ViewBag.group = g;

        return View();
    }
}
