using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSBase.Cadastro.API.Entities;
using MSBase.Core.Infrastructure;

namespace MSBase.Cadastro.API.Infrastructure.Configuration;

public class PessoaConfiguration : AuditableEntityMap<Pessoa>
{
    public override void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.Property(x => x.Nome)
            .HasColumnType("VARCHAR(200)")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(x => x.Idade).IsRequired();

        base.Configure(builder);
    }
}