namespace DefaultNamespace;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsnyc();
    Task<Person> GetByIdAsncy(int id);
    Task AddAsncy(Person person);
    Task UpdateAsncy(Person person);
    Task DeleteAsncy(int id);
}