using System.Reflection;
using AuditoriaAPI.Domain;
using CQRS.Core.API;
using CQRS.Core.Infrastructure;
using CQRS.Core.Infrastructure.Kafka;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaAPI.Infrasctructure
{
    public class AuditoriaDbContext : EfDbContext
    {
        public AuditoriaDbContext(IEnvironment environment, IKafkaBroker kafkaBroker) : base(environment, kafkaBroker)
        {
        }

        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<AuditoriaPropriedade> AuditoriaPropriedades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Auditoria");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
