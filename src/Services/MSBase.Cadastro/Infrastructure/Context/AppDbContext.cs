using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MSBase.Cadastro.API.Entities;
using MSBase.Core.API;
using MSBase.Core.Infrastructure;
using MSBase.Core.RabbitMq;

namespace MSBase.Cadastro.API.Infrastructure.Context;

public class AppDbContext : EfDbContext
{
    public AppDbContext(IEnvironment environment, RabbitMqProducer rabbitMqProducer) : base(environment, rabbitMqProducer)
    {
    }

    public DbSet<Person> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("Cadastros");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}