using System.ComponentModel.DataAnnotations;

namespace WebApp1.DTO.Account;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}