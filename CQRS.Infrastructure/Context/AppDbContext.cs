using System.Reflection;
using CQRS.Core.Bootstrap;
using CQRS.Core.Infrastructure;
using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Context
{
    public class AppDbContext : EfDbContext
    {
        public AppDbContext(IEnvironment enviroment) : base(enviroment)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}