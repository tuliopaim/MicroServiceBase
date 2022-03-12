using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MSBase.Auditoria.API.Domain;
using MSBase.Core.API;
using MSBase.Core.Infrastructure;
using MSBase.Core.Infrastructure.RabbitMq;

namespace MSBase.Auditoria.API.Infrasctructure;

public class AuditoriaDbContext : EfDbContext
{
    public AuditoriaDbContext(IEnvironment environment, RabbitMqProducer rabbitMqProducer) : base(environment, rabbitMqProducer)
    {
    }

    public DbSet<AuditoriaEntidade> Auditoria { get; set; }
    public DbSet<AuditoriaPropriedade> AuditoriaPropriedades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("AuditoriaEntidade");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}