using WebApp1.Models;

namespace WebApp1.Interfaces;

public interface ITokenService
{
    string CreateToken(ApplicationUser user);
}