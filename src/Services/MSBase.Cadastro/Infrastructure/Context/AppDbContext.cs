using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MSBase.Cadastro.API.Entities;
using MSBase.Core.API;
using MSBase.Core.Infrastructure;

namespace MSBase.Cadastro.API.Infrastructure.Context;

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