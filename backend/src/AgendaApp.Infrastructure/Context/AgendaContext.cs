// AgendaApp.Infrastructure/Context/AgendaContext.cs
using AgendaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.Infrastructure.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplicamos todas as configurações de entidades definidas neste assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgendaContext).Assembly);
        }
    }
}