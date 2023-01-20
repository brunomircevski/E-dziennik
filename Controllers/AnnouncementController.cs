using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace project.Controllers;

[Authorize]
public class AnnouncementController : Controller
{
    private readonly IDB DB;
    public AnnouncementController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        List<Announcement> announcements = DB.Announcements.Include(x => x.Teacher).OrderByDescending(x => x.Date).Take(20).ToList();

        ViewBag.announcements = announcements;
        ViewBag.teacherId = getTeacherId();

        return View();
    }

    public IActionResult Details(int id)
    {
        Announcement announcement = DB.Announcements.Include(x => x.Teacher).Where(x => x.Id == id).FirstOrDefault();

        ViewBag.announcement = announcement;

        return View();
    }

    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult Delete(int id)
    {
        Announcement a = DB.Announcements.Where(x=>x.Id == id).FirstOrDefault();
        if(a is null) return RedirectToAction("Index", "Announcement");

        if (!User.IsInRole(Roles.Admin))
        {
            if(a.TeacherId != getTeacherId()) return RedirectToAction("AccessDenied", "Account");
        }

        DB.Remove(a);
        DB.SaveChanges();

        return RedirectToAction("Index", "Announcement");
    }

    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult Add()
    {
        return View();
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    public IActionResult Add(Announcement a)
    {
        if (!ModelState.IsValid) return View();

        if (User.IsInRole(Roles.Teacher))
        {
            int teacherId = getTeacherId();
            if (teacherId != 0) a.TeacherId = teacherId;
            else return RedirectToAction("AccessDenied", "Account");
        }

        a.Date = DateTime.Now;

        DB.Announcements.Add(a);
        DB.SaveChanges();

        return RedirectToAction("Index", "Announcement");
    }

    private int getTeacherId()
    {
        if (!User.IsInRole(Roles.Teacher)) return 0;

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
