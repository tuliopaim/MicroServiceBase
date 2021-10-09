using CQRS.Core.Infrastructure;
using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Infrastructure.Configuration
{
    public class PessoaConfiguration : AuditableEntityMap<Pessoa>
    {
        public override void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.Property(x => x.Nome)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            builder.Property(x => x.Idade).IsRequired();

            base.Configure(builder);
        }
    }
}