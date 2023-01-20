using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;

namespace project.Controllers;

public class HomeController : Controller
{
    private readonly IDB DB;
    public HomeController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        List<Announcement> announcements = DB.Announcements.Include(x => x.Teacher).OrderByDescending(x => x.Date).Take(3).ToList();

        ViewBag.announcements = announcements;

        return View();
    }
}
