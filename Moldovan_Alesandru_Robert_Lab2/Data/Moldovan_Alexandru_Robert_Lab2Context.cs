using Microsoft.EntityFrameworkCore;

public class Moldovan_Alexandru_Robert_Lab2Context(DbContextOptions<Moldovan_Alexandru_Robert_Lab2Context> options) : DbContext(options)
{
    public DbSet<Moldovan_Alexandru_Robert_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Moldovan_Alexandru_Robert_Lab2.Models.Publisher> Publisher {  get; set; } = default!;
    public DbSet<Moldovan_Alexandru_Robert_Lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<Moldovan_Alexandru_Robert_Lab2.Models.Category> Category{ get; set; } = default!;

}
