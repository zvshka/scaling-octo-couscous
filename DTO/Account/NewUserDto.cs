namespace WebApp1.DTO.Account;

public class NewUserDto
{
    //Name с большой из-за IdentityUser
    public string? UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}