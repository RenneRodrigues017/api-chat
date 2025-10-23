using APIChat.Models;
using Microsoft.EntityFrameworkCore;

namespace APIChat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<InteracaoIA> InteracoesIA { get; set; }

        public DbSet<Relatorio> Relatorios { get; set; }
    
    }
}
