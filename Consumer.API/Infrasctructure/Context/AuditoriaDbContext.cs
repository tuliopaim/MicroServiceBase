using AuditoriaAPI.Domain;
using CQRS.Core.API;
using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuditoriaAPI.Infrasctructure.Context
{
    public class AuditoriaDbContext : EfDbContext
    {
        public AuditoriaDbContext(IEnvironment environment) : base(environment)
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
