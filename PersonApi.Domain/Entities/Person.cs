using System.ComponentModel.DataAnnotations;

namespace PersonApi.Domain.Entities;

public class Person : User
{
    
    public string Name { get; set; } = null!;
    public int Age { get; set; }
}