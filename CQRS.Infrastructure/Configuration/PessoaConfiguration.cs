using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Infrastructure.Configuration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            builder.Property(x => x.Idade).IsRequired();
        }
    }
}