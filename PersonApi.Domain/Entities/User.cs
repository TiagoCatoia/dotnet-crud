using System.ComponentModel.DataAnnotations;

namespace PersonApi.Domain.Entities;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; private set; } = null!;
    
    public void SetPassword(string plainTextPassword)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
    }
    
    public bool CheckPassword(string plainTextPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, PasswordHash);
    }
}