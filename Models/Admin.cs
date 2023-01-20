using System.ComponentModel.DataAnnotations;

namespace project.Models;

public class Admin : SiteUser
{
    public Admin()
    {
        Role = Roles.Admin;
    }
}