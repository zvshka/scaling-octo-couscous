﻿using System.ComponentModel.DataAnnotations;

namespace WebApp1.DTO.Account;

public class RegisterDto
{
    [Required]
    public string? Username { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}