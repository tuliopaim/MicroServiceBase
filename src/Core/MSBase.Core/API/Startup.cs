using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MSBase.Core.API.Hateoas;
using MSBase.Core.Application.Mediator.Pipeline;
using MSBase.Core.Infrastructure.RabbitMq;
using Serilog;

namespace MSBase.Core.API;

public static class Startup
{
    public static IServiceCollection RegistrarCore(this IServiceCollection services, CoreSettings settings)
    {
        services
            .AddSingleton(settings)
            .RegistrarApi(settings)
            .RegistrarApplication(settings)
            .RegistrarInfrastructure(settings);

        return services;
    }

    private static IServiceCollection RegistrarApi(this IServiceCollection services, CoreSettings settings)
    {
        if (settings.ConfigurarHateoasHelper)
        {
            services.AddScoped<IHateoasHelper, HateoasHelper>();
        }

        services.RegistrarLogging(settings);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IEnvironment, Environment>();

        return services;
    }

    private static IServiceCollection RegistrarLogging(this IServiceCollection services, CoreSettings settings)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(settings.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .CreateLogger();

        services.AddLogging(x => x.AddSerilog());

        return services;
    }

    private static IServiceCollection RegistrarApplication(this IServiceCollection services, CoreSettings settings)
    {
        if (!settings.ConfigurarMediator) return services;

        services.AddMediatR(settings.TipoDoStartup);

        services.AddScoped<Application.Mediator.IMediator, Application.Mediator.Mediator>();
                
        services
            .RegistrarExceptionPipelineBehavior()
            .RegistrarLogPipelineBehavior()
            .RegistrarFailFastPipeline(settings)
            .RegistrarValidators(settings);

        return services;
    }
        
    private static IServiceCollection RegistrarExceptionPipelineBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection RegistrarLogPipelineBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection RegistrarFailFastPipeline(this IServiceCollection services, CoreSettings settings)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection RegistrarValidators(this IServiceCollection services, CoreSettings settings)
    {
        var assembly = settings.TipoDaCamadaDeApplication.Assembly;

        foreach (var validator in AssemblyScanner.FindValidatorsInAssembly(assembly))
        {
            services.AddScoped(validator.InterfaceType, validator.ValidatorType);
        }
            
        return services;
    }

    private static IServiceCollection RegistrarInfrastructure(this IServiceCollection services, CoreSettings settings)
    {
        if (!settings.ConfigurarRabbitMq) return services;

        services.AddSingleton<RabbitMqConnection>();
        services.AddScoped<RabbitMqProducer>();

        return services;
    }
}