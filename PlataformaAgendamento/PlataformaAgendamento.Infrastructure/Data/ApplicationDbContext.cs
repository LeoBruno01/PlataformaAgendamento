using Microsoft.EntityFrameworkCore;
using PlataformaAgendamento.Domain.Entities;

namespace PlataformaAgendamento.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // aq avisa que a classe Usuario vai virar tabela "Usuarios" no banco
    public DbSet<Usuario> Usuarios { get; set; }
}