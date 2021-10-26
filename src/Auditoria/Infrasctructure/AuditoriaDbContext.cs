using System.Reflection;
using Auditoria.API.Domain;
using CQRS.Core.API;
using CQRS.Core.Infrastructure;
using CQRS.Core.Infrastructure.Kafka;
using Microsoft.EntityFrameworkCore;

namespace Auditoria.API.Infrasctructure
{
    public class AuditoriaDbContext : EfDbContext
    {
        public AuditoriaDbContext(IEnvironment environment, IKafkaBroker kafkaBroker) : base(environment, kafkaBroker)
        {
        }

        public DbSet<Domain.Auditoria> Auditoria { get; set; }
        public DbSet<AuditoriaPropriedade> AuditoriaPropriedades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Auditoria");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
