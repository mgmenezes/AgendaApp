using AgendaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaApp.Infrastructure.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(c => c.Email)
                .IsUnique();

            // filtro global para só trazer registros ativos por padrão
            builder.HasQueryFilter(c => c.Ativo);
        }
    }
}