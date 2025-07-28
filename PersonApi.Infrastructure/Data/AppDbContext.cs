using Microsoft.EntityFrameworkCore;
using PersonApi.Domain.Entities;

namespace PersonApi.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Person> People => Set<Person>();
}