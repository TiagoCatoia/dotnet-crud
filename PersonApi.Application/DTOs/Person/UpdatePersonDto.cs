using System.ComponentModel.DataAnnotations;

namespace PersonApi.Application.DTOs.Person;

public record UpdatePersonDto
{
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    public string? Name { get; init; } = null!;

    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
    public int? Age { get; init; }
    
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(150, ErrorMessage = "Email must be at most 150 characters")]
    public string? Email { get; init; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    public string? Password { get; init; }
}