using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Core.Infrastructure
{
    public abstract class AuditableEntityMap<TEntity> : EntityMap<TEntity> where TEntity : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(x => x.DataCriacao)
                .HasColumnName("DataCriacao")
                .IsRequired();

            builder
                .Property(x => x.DataAlteracao)
                .HasColumnName("DataAlteracao");
            
            base.Configure(builder);
        }
    }
}