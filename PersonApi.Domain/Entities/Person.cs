using System.ComponentModel.DataAnnotations;

namespace PersonApi.Domain.Entities;

public class Person
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public int Age { get; set; }
}