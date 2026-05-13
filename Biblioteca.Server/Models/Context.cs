using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Server.Models;
public class BibliotecaDbContext : DbContext
{
    //miembro constructor 
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
    {
    }

    //Data Base Set crea tablas en
    //funcion a las clases declaradas
    //en los otros archivos
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    //aca va el tema de OnModelCreating que es para configurar las tablas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .Property(a => a.Username)
            .IsRequired()
            .HasMaxLength(10);
        modelBuilder
            .Entity<User>()
            .Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(8);
    }
}
