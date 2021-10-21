using AuditoriaAPI.Domain;
using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CQRS.Core.Domain;

namespace AuditoriaAPI.Infrasctructure.Configs
{
    public class AuditoriaPropriedadeConfig : AuditableEntityMap<AuditoriaPropriedade>
    {
        public override void Configure(EntityTypeBuilder<AuditoriaPropriedade> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.NomeDaPropriedade)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(x => x.NomeDaColuna)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(x => x.ValorAntigo)
                .HasColumnType("text");

            builder.Property(x => x.ValorNovo)
                .HasColumnType("text");
        }
        }
}
