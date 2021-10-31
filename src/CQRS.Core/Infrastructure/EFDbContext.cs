﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.API;
using CQRS.Core.Infrastructure.Auditoria;
using CQRS.Core.Infrastructure.Kafka;
using CQRS.Core.Infrastructure.Kafka.KafkaEventTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CQRS.Core.Infrastructure
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
                // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            FillAddedAuditableProperties(entry);

                            break;
                        }
                    case EntityState.Modified:
                        {
                            FillModifiedAuditableProperties(entry);

                            break;
                        }
                }
            }

            return await SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditoriaEvent = ChangeTracker.ObterEventoDeAuditoria();

            var linhasAfetadas = await base.SaveChangesAsync(cancellationToken);

            if (linhasAfetadas > 0 && auditoriaEvent.Auditorias.Any())
            {
                await EnviarMensagemDeAuditoria(auditoriaEvent, cancellationToken);
            }

            return linhasAfetadas;
        }

        private async Task EnviarMensagemDeAuditoria(AuditoriaEvent auditoriaEvent, CancellationToken cancellationToken)
        {
            await _kafkaBroker.PublishAsync(KafkaTopics.AuditoriaTopic, AuditoriaEventTypes.NovaAuditoria, auditoriaEvent, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_environment == null) return;

            var connectionString = _environment[ConnectionStringKey];

            optionsBuilder.UseNpgsql(connectionString, opt => opt.EnableRetryOnFailure());
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
    }
}