using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MSBase.Auditoria.API.Domain;
using MSBase.Core.Infrastructure;
using MSBase.Core.RabbitMq;

namespace MSBase.Auditoria.API.Infrasctructure;

public class AuditoriaDbContext : EfDbContext
{
    public AuditoriaDbContext(RabbitMqProducer rabbitMqProducer, IConfiguration configuration)
        : base(rabbitMqProducer, configuration)
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