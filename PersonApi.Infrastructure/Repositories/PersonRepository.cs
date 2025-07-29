using Microsoft.EntityFrameworkCore;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Repositories;
using PersonApi.Infrastructure.Data;

namespace PersonApi.Infrastructure.Repositories;

public class PersonRepository(AppDbContext _context) : IPersonRepository
{
    public async Task<IEnumerable<Person>> GetAllAsync() => await _context.People.ToListAsync();

    public async Task<Person?> GetByIdAsync(Guid id) => await _context.People.FindAsync(id);

    public async Task AddAsync(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Person person)
    {
        _context.People.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var person = await _context.People.FindAsync(id);

        if (person != null)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}