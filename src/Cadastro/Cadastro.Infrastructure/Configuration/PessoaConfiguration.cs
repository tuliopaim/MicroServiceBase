using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSBase.Core.Infrastructure;

namespace Cadastro.Infrastructure.Configuration
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