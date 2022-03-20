using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSBase.Core.API;
using MSBase.Core.Cqrs.Pipelines;
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
            .AddHttpContextAccessor()
            .AddEnvironment()
            .AddHateoas(coreConfiguration)
            .AddCqrs(coreConfiguration)
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

    private static IServiceCollection AddCqrs(this IServiceCollection services, CoreConfiguration configuration)
    {
        if (!configuration.ConfigureCqrs) return services;

        if (configuration.CqrsAssemblies is not { Length: > 0 })
        {
            throw new MissingMemberException(nameof(CoreConfiguration), nameof(configuration.CqrsAssemblies));
        }

        services.AddScoped<Cqrs.Mediator.IMediator, Cqrs.Mediator.Mediator>();
        services.AddMediatR(configuration.CqrsAssemblies);

        services
            .AddExceptionPipelineBehavior()
            .AddLogPipelineBehavior()
            .AddFailFastPipeline(configuration)
            .AddValidators(configuration);

        return services;
    }

    private static IServiceCollection AddExceptionPipelineBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection AddLogPipelineBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection AddFailFastPipeline(this IServiceCollection services, CoreConfiguration configuration)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services, CoreConfiguration configuration)
    {
        foreach (var assemblyScanResult in AssemblyScanner.FindValidatorsInAssemblies(configuration.CqrsAssemblies))
        {
            services.AddScoped(assemblyScanResult.InterfaceType, assemblyScanResult.ValidatorType);
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