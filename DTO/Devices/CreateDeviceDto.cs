﻿using System.ComponentModel.DataAnnotations;

namespace WebApp1.DTO.Devices;

public class CreateDeviceDto
{

    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MaxLength(50, ErrorMessage = "Название не может содержать больше 50 символов")]
    [MinLength(5, ErrorMessage = "Название не может содержать менее 5 символов")]
    public string Name { get; set; } = string.Empty;
}