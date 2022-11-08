using EasyCqrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSBase.Core.API;
using MSBase.Core.Queries;
using MSBase.Core.RabbitMq;
using Serilog;

namespace MSBase.Core.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Minimal core support
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddCore(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<CoreConfiguration> options = null)
    {
        var coreConfiguration = new CoreConfiguration(configuration);

        options?.Invoke(coreConfiguration);

        if (coreConfiguration.CqrsAssemblies is { Length: > 0 })
        {
            services.AddCqrs(coreConfiguration.CqrsAssemblies);
        }

        return services
            .AddLogging(coreConfiguration)
            .AddHttpContextAccessor()
            .AddHateoas(coreConfiguration)
            .AddRabbitMq(coreConfiguration, configuration);
    }
    
    private static IServiceCollection AddLogging(this IServiceCollection services, CoreConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .CreateLogger();

        services.AddLogging(x => x.AddSerilog());

        return services;
    }

    private static IServiceCollection AddHateoas(this IServiceCollection services, CoreConfiguration configuration)
    {
        if (configuration.ConfigureHateoas)
        {
            services.AddScoped<IHateoasHelper, HateoasHelper>();
        }
        
        return services;
    }
    
    private static IServiceCollection AddRabbitMq(
        this IServiceCollection services,
        CoreConfiguration coreConfiguration,
        IConfiguration configuration)
    {
        if (!coreConfiguration.ConfigureRabbitMq) return services;

        services.AddSingleton<RabbitMqConnection>();
        services.AddScoped<RabbitMqProducer>();
        services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));

        return services;
    }

}
