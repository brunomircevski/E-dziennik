using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace project.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IDB DB;
    public AdminController(IDB _DB)
    {
        DB = _DB;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult UserList()
    {
        List<SiteUser> users = new List<SiteUser>();

        users.AddRange(DB.Students.ToList());
        users.AddRange(DB.Parents.ToList());
        users.AddRange(DB.Teachers.ToList());
        users.AddRange(DB.Admins.ToList());

        ViewBag.users = users;

        return View();
    }

    public IActionResult DeleteUser(int id, string role)
    {
        if (role == Roles.Student) DB.Remove(DB.Students.Single(x => x.Id == id));
        else if (role == Roles.Teacher)
        {
            if (DB.Groups.Where(x => x.TeacherId == id).FirstOrDefault() is null)
                DB.Remove(DB.Teachers.Single(x => x.Id == id));
        }
        else if (role == Roles.Parent)
        {
            if (DB.Students.Where(x => x.Parent.Id == id).FirstOrDefault() is null)
                DB.Remove(DB.Parents.Single(x => x.Id == id));
        }
        else if (role == Roles.Admin)
        {
            if (DB.Admins.Count() > 2)
                DB.Remove(DB.Admins.Single(x => x.Id == id));
        }

        DB.SaveChanges();

        return RedirectToAction("UserList", "Admin");
    }

    public IActionResult RegisterUser(string role)
    {
        ViewBag.role = role;
        return View();
    }

    [HttpPost]
    public IActionResult RegisterUser(CreateUser createUser)
    {
        if (!ModelState.IsValid) return View();

        ViewBag.error = "true";

        switch (createUser.Role)
        {
            case "teacher":
                Teacher t = new Teacher();
                t.Email = createUser.Email;
                t.Name = createUser.Name;

                if (PrepareUser(t, createUser.Password))
                {
                    DB.Teachers.Add(t);
                    DB.SaveChanges();
                    ViewBag.error = "false";
                }
                break;
            case "parent":
                Parent p = new Parent();
                p.Email = createUser.Email;
                p.Name = createUser.Name;

                if (PrepareUser(p, createUser.Password))
                {
                    DB.Parents.Add(p);
                    DB.SaveChanges();
                    ViewBag.error = "false";
                }
                break;
            case "admin":
                Admin a = new Admin();
                a.Email = createUser.Email;
                a.Name = createUser.Name;

                if (PrepareUser(a, createUser.Password))
                {
                    DB.Admins.Add(a);
                    DB.SaveChanges();
                    ViewBag.error = "false";
                }
                break;
        }

        return View();
    }

    public IActionResult RegisterStudent()
    {
        List<SelectListItem> parents = DB.Parents
            .Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name + " (" + a.Email + ")",
            }).ToList();

        ViewBag.parents = parents;

        return View();
    }

    [HttpPost]
    public IActionResult RegisterStudent(CreateStudent createStudent)
    {
        ViewBag.error = "true";

        if (ModelState.IsValid)
        {
            Student s = new Student();
            s.Email = createStudent.Email;
            s.Name = createStudent.Name;

            if (PrepareUser(s, createStudent.Password))
            {
                s.Parent = DB.Parents.Where(x => x.Id == createStudent.ParentId).FirstOrDefault();
                if (s.Parent is not null)
                {
                    DB.Students.Add(s);
                    DB.SaveChanges();
                    ViewBag.error = "false";
                }
            }
        }

        return RegisterStudent();
    }

    private Boolean PrepareUser(SiteUser user, string password)
    {
        if (user.Email is null) return false;
        if (user.Name is null) return false;

        if (DB.Students.Where(x => x.Email == user.Email).FirstOrDefault() is not null) return false;
        if (DB.Teachers.Where(x => x.Email == user.Email).FirstOrDefault() is not null) return false;
        if (DB.Parents.Where(x => x.Email == user.Email).FirstOrDefault() is not null) return false;
        if (DB.Admins.Where(x => x.Email == user.Email).FirstOrDefault() is not null) return false;

        user.Salt = new byte[128 / 8];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetNonZeroBytes(user.Salt);
        }

        user.PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: user.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return true;
    }

    public IActionResult Password(int id, string role)
    {
        SiteUser user = null;
        if (role == Roles.Admin) user = DB.Admins.Where(x => x.Id == id).FirstOrDefault();
        else if (role == Roles.Student) user = DB.Students.Where(x => x.Id == id).FirstOrDefault();
        else if (role == Roles.Teacher) user = DB.Teachers.Where(x => x.Id == id).FirstOrDefault();
        else if (role == Roles.Parent) user = DB.Parents.Where(x => x.Id == id).FirstOrDefault();

        if (user is null) return RedirectToAction("AccessDenied", "Account");

        ViewBag.user = user;

        return View();
    }

    [HttpPost]
    public IActionResult Password(CreateUser user)
    {
        if (!ModelState.IsValid) return Password(user.Id, user.Role);

        SiteUser DBuser = null;
        if (user.Role == Roles.Admin) DBuser = DB.Admins.Where(x => x.Id == user.Id).FirstOrDefault();
        else if (user.Role == Roles.Student) DBuser = DB.Students.Where(x => x.Id == user.Id).FirstOrDefault();
        else if (user.Role == Roles.Teacher) DBuser = DB.Teachers.Where(x => x.Id == user.Id).FirstOrDefault();
        else if (user.Role == Roles.Parent) DBuser = DB.Parents.Where(x => x.Id == user.Id).FirstOrDefault();

        if (DBuser is null) return RedirectToAction("AccessDenied", "Account");

        DBuser.PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: user.Password,
            salt: DBuser.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        DB.SaveChanges();

        return RedirectToAction("UserList", "Admin");
    }
}
