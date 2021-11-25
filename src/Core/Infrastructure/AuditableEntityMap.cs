using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure
{
    public abstract class AuditableEntityMap<TEntity> : EntityMap<TEntity> where TEntity : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(x => x.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(x => x.DataAlteracao)
                .HasColumnType("date")
                .HasColumnName("DataAlteracao");

            base.Configure(builder);
        }
    }
}