using System.Reflection;
using Cadastro.Domain.Entities;
using CQRS.Core.API;
using CQRS.Core.Infrastructure;
using CQRS.Core.Infrastructure.Kafka;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Infrastructure.Context
{
    public class AppDbContext : EfDbContext
    {
        public AppDbContext(IEnvironment enviroment, IKafkaBroker kafkaBroker) : base(enviroment, kafkaBroker)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("CQRS");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}