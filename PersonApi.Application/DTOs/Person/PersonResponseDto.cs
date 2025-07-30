namespace PersonApi.Application.DTOs.Person;

public record PersonResponseDto
{
    public Guid Id { get; init; }
    public string Email { get; set; } = null!;
    public string Name { get; init; } = null!;
    public int Age { get; init; }
}