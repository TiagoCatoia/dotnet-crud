using System.ComponentModel.DataAnnotations;

namespace PersonApi.Domain.Entities;

public class Person
{
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Age is required")]
    [Range(1, 120, ErrorMessage = "Age must be between 0 and 120")]
    public int Age { get; set; }
}