using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.DataAccess;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace project.Controllers;

public class AccountController : Controller
{
    private readonly IDB DB;
    public AccountController(IDB _DB)
    {
        DB = _DB;
    }

    [Authorize]
    public IActionResult Index()
    {
        try
        {
            string role = User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
            int id = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            return RedirectToAction("Profile", "Account", new { id = id, role = role });
        }
        catch
        {
            return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    public IActionResult NotAssigned()
    {
        return View();
    }

    [Authorize]
    public IActionResult Profile(int id, string role)
    {
        if (id == 0 || role == null) return RedirectToAction("Index", "Account");

        if (role == Roles.Student)
        {
            Student s = DB.Students.Include(x => x.Parent).Include(x => x.Group).Single(x => x.Id == id);
            if (s is not null)
            {
                ViewBag.user = s;
                return View();
            }
        }
        else if (role == Roles.Parent)
        {
            Parent p = DB.Parents.Include(x => x.Students).Single(x => x.Id == id);

            foreach (Student s in p.Students)
            {
                s.Group = DB.Groups.Where(x => x.Students.Contains(s)).FirstOrDefault();
            }

            if (p is not null)
            {
                ViewBag.user = p;
                return View();
            }
        }
        else if (role == Roles.Teacher)
        {
            Teacher t = DB.Teachers.Include(x => x.Group).Include(x => x.Subjects).Single(x => x.Id == id);
            if (t is not null)
            {
                ViewBag.user = t;
                return View();
            }
        }
        else if (role == Roles.Admin)
        {
            Admin a = DB.Admins.Single(x => x.Id == id);
            if (a is not null)
            {
                ViewBag.user = a;
                return View();
            }
        }

        return RedirectToAction("Index", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUser u)
    {
        if (!ModelState.IsValid) return View();

        SiteUser user = VerifyUser(u.Email, u.Password);

        if (user is not null)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        ViewBag.msg = "Zły login lub hasło. Spróbuj ponownie.";
        return View();
    }

    [Authorize]
    public IActionResult Edit(int id, string role, bool? error)
    {
        if (id == 0 || role == null) return RedirectToAction("Index", "Account");

        string name = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
        string email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;

        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(email)) return RedirectToAction("Index", "Account");

        ViewBag.name = name;
        ViewBag.email = email;
        ViewBag.id = id;
        ViewBag.role = role;
        ViewBag.error = error;

        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Edit(CreateUser user)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.error = true;
            ViewBag.id = user.Id;
            ViewBag.role = user.Role;
            return View();
        }

        SiteUser DBuser = null;

        if (user.Role == Roles.Student)
        {
            DBuser = DB.Students.Single(x => x.Id == user.Id);
        }
        else if (user.Role == Roles.Parent)
        {
            DBuser = DB.Parents.Single(x => x.Id == user.Id);
        }
        else if (user.Role == Roles.Teacher)
        {
            DBuser = DB.Teachers.Single(x => x.Id == user.Id);
        }
        else if (user.Role == Roles.Admin)
        {
            DBuser = DB.Admins.Single(x => x.Id == user.Id);
        }

        if (DBuser is null) return RedirectToAction("AccessDenied", "Account");

        DBuser.Email = user.Email;
        DBuser.Name = user.Name;

        DB.SaveChanges();

        return RedirectToAction("Profile", "Account");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private SiteUser VerifyUser(string email, string password)
    {
        SiteUser user = DB.Students.Where(x => x.Email == email).FirstOrDefault();
        if (user is not null)
        {
            user.Role = Roles.Student;
            if (VerifyPassword(user, password)) return user;
            else return null;
        }

        user = DB.Teachers.Where(x => x.Email == email).FirstOrDefault();
        if (user is not null)
        {
            user.Role = Roles.Teacher;
            if (VerifyPassword(user, password)) return user;
            else return null;
        }

        user = DB.Parents.Where(x => x.Email == email).FirstOrDefault();
        if (user is not null)
        {
            user.Role = Roles.Parent;
            if (VerifyPassword(user, password)) return user;
            else return null;
        }

        user = DB.Admins.Where(x => x.Email == email).FirstOrDefault();
        if (user is not null)
        {
            user.Role = Roles.Admin;
            if (VerifyPassword(user, password)) return user;
            else return null;
        }

        return null;
    }

    private Boolean VerifyPassword(SiteUser user, string password)
    {
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: user.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        if (hash == user.PasswordHash) return true;
        return false;
    }

}
