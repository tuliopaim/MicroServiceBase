using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSBase.Auditoria.API.Domain;
using MSBase.Core.Infrastructure;

namespace MSBase.Auditoria.API.Infrasctructure.Configs
{
    public class AuditoriaEntidadeConfig : AuditableEntityMap<AuditoriaEntidade>
    {
        public override void Configure(EntityTypeBuilder<AuditoriaEntidade> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.TipoAuditoria)
                .HasConversion<string>()
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.NomeEntidade)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(x => x.NomeTabela)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.HasMany(x => x.Propriedades)
                .WithOne(p => p.AuditoriaEntidade)
                .HasForeignKey(p => p.AuditoriaId);
        }
    }
}
