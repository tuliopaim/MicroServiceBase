﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSBase.Core.Domain;

namespace MSBase.Core.Infrastructure
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