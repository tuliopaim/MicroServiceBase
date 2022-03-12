using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSBase.Core.Infrastructure;

namespace MSBase.Auditoria.API.Infrasctructure.Configs
{
    public class AuditoriaConfig : AuditableEntityMap<Domain.Auditoria>
    {
        public override void Configure(EntityTypeBuilder<Domain.Auditoria> builder)
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
                .WithOne(p => p.Auditoria)
                .HasForeignKey(p => p.AuditoriaId);
        }
    }
}
