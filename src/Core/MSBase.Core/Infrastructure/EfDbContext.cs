using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MSBase.Core.API;
using MSBase.Core.Domain;
using MSBase.Core.Extensions;
using MSBase.Core.RabbitMq;
using MSBase.Core.RabbitMq.Messages;

namespace MSBase.Core.Infrastructure;

public class EfDbContext : DbContext, IUnitOfWork
{
    private readonly RabbitMqProducer _rabbitMqProducer;
    private readonly IConfiguration _configuration;

    private const string _connectionStringKey = "ConnectionStrings:DefaultConnection";

    public EfDbContext(RabbitMqProducer rabbitMqProducer, IConfiguration configuration)
    {
        _rabbitMqProducer = rabbitMqProducer;
        _configuration = configuration;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State is EntityState.Added)
            {
                FillAddedAuditableProperties(entry);
            }
            else if (entry.State is EntityState.Modified)
            {
                FillModifiedAuditableProperties(entry);
            }
        }

        return await SaveChangesAsync(cancellationToken);
    }

    private static void FillAddedAuditableProperties(EntityEntry entry)
    {
        if (entry.Entity.GetType().GetProperty(nameof(AuditableEntity.DataCriacao)) != null)
            entry.Property(nameof(AuditableEntity.DataCriacao)).CurrentValue = DateTime.Now;
    }

    private static void FillModifiedAuditableProperties(EntityEntry entry)
    {
        if (entry.Entity.GetType().GetProperty(nameof(AuditableEntity.DataAlteracao)) != null)
            entry.Property(nameof(AuditableEntity.DataAlteracao)).CurrentValue = DateTime.Now;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditoriaEvent = ChangeTracker.ObterEventoDeAuditoria();

        var linhasAfetadas = await base.SaveChangesAsync(cancellationToken);

        if (linhasAfetadas > 0 && auditoriaEvent.Auditorias.Any())
        {
            _rabbitMqProducer.Publish(auditoriaEvent, MessageType.NovaAuditoria, RoutingKeys.NovaAuditoria);
        }

        return linhasAfetadas;
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration[_connectionStringKey];

        optionsBuilder.UseNpgsql(connectionString, opt => opt.EnableRetryOnFailure());
    }

}