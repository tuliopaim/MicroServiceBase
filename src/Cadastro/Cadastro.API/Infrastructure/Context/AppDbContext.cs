using System.Reflection;
using Cadastro.API.Entities;
using Core.API;
using Core.Infrastructure;
using Core.Infrastructure.Kafka;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.API.Infrastructure.Context
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

            modelBuilder.HasDefaultSchema("Cadastros");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}