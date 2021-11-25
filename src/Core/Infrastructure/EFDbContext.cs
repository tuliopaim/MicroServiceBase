using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MSBase.Core.API;
using MSBase.Core.Infrastructure.Auditoria;
using MSBase.Core.Infrastructure.Kafka;
using MSBase.Core.Infrastructure.Kafka.KafkaEventTypes;

namespace MSBase.Core.Infrastructure
{
    public class EfDbContext : DbContext, IUnitOfWork
    {
        private readonly IEnvironment _environment;
        private readonly IKafkaBroker _kafkaBroker;

        private const string ConnectionStringKey = "ConnectionStrings:DefaultConnection";

        public EfDbContext(IEnvironment environment, IKafkaBroker kafkaBroker)
        {
            _environment = environment;
            _kafkaBroker = kafkaBroker;
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
            if (entry.Entity.GetType().GetProperty("DataCriacao") != null)
                entry.Property("DataCriacao").CurrentValue = DateTime.Now;
        }

        private static void FillModifiedAuditableProperties(EntityEntry entry)
        {
            if (entry.Entity.GetType().GetProperty("DataAlteracao") != null)
                entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditoriaEvent = ChangeTracker.ObterEventoDeAuditoria();

            var linhasAfetadas = await base.SaveChangesAsync(cancellationToken);

            if (linhasAfetadas > 0 && auditoriaEvent.Auditorias.Any())
            {
                await PublicarAuditoria(auditoriaEvent, cancellationToken);
            }

            return linhasAfetadas;
        }

        private async Task PublicarAuditoria(AuditoriaEvent auditoriaEvent, CancellationToken cancellationToken)
        {
            await _kafkaBroker.PublishAsync(KafkaTopics.NovaAuditoria, AuditoriaEventTypes.NovaAuditoria, auditoriaEvent, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_environment == null) return;

            var connectionString = _environment[ConnectionStringKey];

            optionsBuilder.UseNpgsql(connectionString, opt => opt.EnableRetryOnFailure());
        }

    }
}