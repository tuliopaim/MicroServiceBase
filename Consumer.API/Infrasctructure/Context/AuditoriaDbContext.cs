using CQRS.Core.API;
using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Auditoria.API.Infrasctructure.Context
{
    public class AuditoriaDbContext : EfDbContext
    {
        public AuditoriaDbContext(IEnvironment environment) : base(environment)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
