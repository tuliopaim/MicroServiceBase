using Auditoria.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSBase.Core.Infrastructure;

namespace Auditoria.API.Infrasctructure.Configs
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
