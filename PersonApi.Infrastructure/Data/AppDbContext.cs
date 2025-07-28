namespace DefaultNamespace;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOption<AppDbContex> options) : base(options) {}

    public DbSet<Person> People => Set<Person>(); // get { return Set<Person>(); }
}