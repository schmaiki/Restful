namespace Restful.Database;

public class PersonDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Color> Colors { get; set; }

    public PersonDbContext(DbContextOptions<PersonDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connection = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder
            .UseSqlServer(connection);
    }
}