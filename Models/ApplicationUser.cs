using Microsoft.AspNetCore.Identity;

namespace WebApp1.Models;

public class ApplicationUser : IdentityUser
{
    public string Email { get; set; }
}