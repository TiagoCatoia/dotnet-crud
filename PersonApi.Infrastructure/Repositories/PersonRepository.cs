using Microsoft.EntityFrameworkCore;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Repositories;
using PersonApi.Infrastructure.Data;

namespace PersonApi.Infrastructure.Repositories;

public class PersonRepository(AppDbContext context) : IPersonRepository
{
    public async Task<IEnumerable<Person>> GetAllAsync() => await context.People.ToListAsync();

    public async Task<Person?> GetByIdAsync(Guid id) => await context.People.FindAsync(id);
    
    public async Task<Person?> GetByEmailAsync(string email) => await context.People.FindAsync(email);

    public async Task AddAsync(Person person)
    {
        context.People.Add(person);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Person person)
    {
        context.People.Update(person);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var person = await context.People.FindAsync(id);

        if (person != null)
        {
            context.People.Remove(person);
            await context.SaveChangesAsync();
        }
    }
}