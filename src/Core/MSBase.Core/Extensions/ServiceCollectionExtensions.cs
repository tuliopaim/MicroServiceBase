using EasyCqrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSBase.Core.API;
using MSBase.Core.Hateoas;
using MSBase.Core.RabbitMq;
using Serilog;
using Environment = MSBase.Core.API.Environment;

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
        
        return services
            .AddLogging(coreConfiguration)
            .AddCqrs(coreConfiguration.CqrsAssemblies)
            .AddHttpContextAccessor()
            .AddEnvironment()
            .AddHateoas(coreConfiguration)
            .AddRabbitMq(coreConfiguration);
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

    private static IServiceCollection AddEnvironment(this IServiceCollection services)
    {
        services.AddSingleton<IEnvironment, Environment>();

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
    
    private static IServiceCollection AddRabbitMq(this IServiceCollection services, CoreConfiguration configuration)
    {
        if (!configuration.ConfigureRabbitMq) return services;

        services.AddSingleton<RabbitMqConnection>();
        services.AddScoped<RabbitMqProducer>();

        return services;
    }

}