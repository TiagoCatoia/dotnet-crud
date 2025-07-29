using PersonApi.Domain.Entities;

namespace PersonApi.Domain.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(Guid id);
    Task AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(Guid id);
}