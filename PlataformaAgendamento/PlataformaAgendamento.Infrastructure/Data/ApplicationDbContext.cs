using Microsoft.EntityFrameworkCore;
using PlataformaAgendamento.Domain.Entities;

namespace PlataformaAgendamento.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // aq avisa que a classe Usuario vai virar tabela no banco
    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Servico> Servicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // o preço vai ter o formato de dinheiro correto
        modelBuilder.Entity<Servico>()
            .Property(s => s.Preco)
            .HasColumnType("decimal(18,2)");
    }
}