using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace project.Controllers;

[Authorize]
public class MessageController : Controller
{
    private readonly IDB DB;
    public MessageController(IDB _DB)
    {
        DB = _DB;
    }

    [Authorize(Roles = "Teacher")]
    public IActionResult Index()
    {
        List<Message> messages = DB.Messages
            .Include(x => x.Group)
            .Where(x => x.TeacherId == getUserId())
            .OrderByDescending(x => x.Date)
            .ToList();

        ViewBag.messages = messages;

        return View();
    }

    [Authorize(Roles = "Teacher")]
    public IActionResult Add(int ?groupId)
    {
        List<SelectListItem> groups = DB.Groups
            .Include(x => x.Teacher)
            .Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Name + " (" + g.Teacher.Name + ")",
                Selected = (g.Id == groupId)
            }).ToList();

        ViewBag.groups = groups;

        return View();
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public IActionResult Add(Message message)
    {
        message.Date = DateTime.Now;
        message.TeacherId = getUserId();

        if(message.TeacherId != 0) {
            DB.Messages.Add(message);
            DB.SaveChanges();
        }

        return RedirectToAction("Index", "Message");
    }

    [Authorize(Roles = "Teacher")]
    public IActionResult Delete(int id)
    {
        Message message = DB.Messages
            .Where(x => x.Id == id && x.TeacherId == getUserId())
            .FirstOrDefault();

        if(message is not null) {
            DB.Remove(message);
            DB.SaveChanges();
        }

        return RedirectToAction("Index", "Message");
    }

    [Authorize(Roles = "Student")]
    public IActionResult Group()
    {
        int ?groupId = DB.Students.Where(x => x.Id == getUserId()).Select(x => x.GroupId).FirstOrDefault();

        if(groupId is null || groupId == 0) return RedirectToAction("AccessDenied", "Account");

        Group group = DB.Groups
            .Include(x => x.Messages)
            .ThenInclude(x => x.Teacher)
            .Where(x => x.Id == groupId)
            .FirstOrDefault();

        ViewBag.group = group;

        return View();
    }

    private int getUserId()
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
