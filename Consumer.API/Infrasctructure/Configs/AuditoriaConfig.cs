using AuditoriaAPI.Domain;
using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaAPI.Infrasctructure.Configs
{
    public class AuditoriaConfig : AuditableEntityMap<Auditoria>
    {
        public override void Configure(EntityTypeBuilder<Auditoria> builder)
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
