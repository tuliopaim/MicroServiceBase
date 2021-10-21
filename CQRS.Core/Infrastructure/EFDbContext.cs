using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CQRS.Core.Infrastructure
{
    public class EfDbContext : DbContext, IUnitOfWork
    {
        private readonly IEnvironment _environment;

        private const string ConnectionStringKey = "ConnectionStrings:DefaultConnection";

        public EfDbContext(IEnvironment environment)
        {
            _environment = environment;
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
            if (linhasAfetadas > 0)
            {
                //disparar evento

            }

            return linhasAfetadas;
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