﻿using System.ComponentModel.DataAnnotations;

namespace PersonApi.Application.DTOs.Person;

public class PersonLoginRequestDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = null!;
}