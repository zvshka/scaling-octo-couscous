using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp1.DTO.Account;
using WebApp1.Interfaces;
using WebApp1.Models;

namespace WebApp1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<ApplicationUser> _signinManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signinManager = signInManager;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var appUser = User.FindFirst(ClaimTypes.GivenName);
        if (appUser == null) return Unauthorized();
        var userData = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == appUser.Value);
        return Ok(new NewUserDto
        {
            UserName = userData.UserName,
            Email = userData.Email,
        });

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("Имя пользователя не найдено или не правильный пароль!");

        var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Имя пользователя не найдено или не правильный пароль!");

        return Ok(
            new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            }
        );
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!createdUser.Succeeded) return StatusCode(400, createdUser.Errors);
            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if (roleResult.Succeeded)
            {
                return Ok(
                    new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    }
                );
            }

            return StatusCode(400, roleResult.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}