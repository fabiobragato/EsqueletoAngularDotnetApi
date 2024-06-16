using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } // Usuarios será o nome da tabela criada no banco de dados

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios"); // Define o nome da tabela no banco de dados
            entity.HasKey(e => e.Id); // Define a chave primária
            entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Define que o Id será gerado automaticamente
            entity.Property(e => e.Email).IsRequired(); // Define que o Email é obrigatório
            entity.Property(e => e.EmailConfirmado).IsRequired().HasDefaultValue(false); // Define que o Email é obrigatório e o valor default é false
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.PasswordSalt).IsRequired();
            entity.Property(e => e.Celular).IsRequired();
            entity.Property(e => e.EhAdmin).IsRequired();
        });
    }
}
