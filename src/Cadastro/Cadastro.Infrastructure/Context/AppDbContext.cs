using System.Reflection;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MSBase.Core.API;
using MSBase.Core.Infrastructure;
using MSBase.Core.Infrastructure.Kafka;

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