using System.Reflection;
using Core.API.Hateoas;
using Core.Application.Mediator.Pipeline;
using Core.Infrastructure.Kafka;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core.API
{
    public static class Startup
    {
        public static IServiceCollection RegistrarCore(this IServiceCollection services, CoreSettings settings)
        {
            services
                .AddSingleton(settings)
                .RegistrarApi(settings)
                .RegistrarApplication(settings)
                .RegistrarCrossCutting(settings);

            return services;
        }

        private static IServiceCollection RegistrarApi(this IServiceCollection services, CoreSettings settings)
        {
            if (settings.ConfigurarConsumerHandlers)
            {
                RegisterConsumers(services, settings);
            }

            if (settings.ConfigurarHateoasHelper)
            {
                services.AddScoped<IHateoasHelper, HateoasHelper>();
            }

            services.RegistrarLogging(settings);

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

        private static void RegisterConsumers(IServiceCollection services, CoreSettings settings)
        {
            services.AddHostedService<RunConsumersService>();

            var consumersDoAssembly = settings.TipoDoStartup.Assembly
                .ExportedTypes
                .Select(t => t.GetTypeInfo())
                .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IConsumerHandler)))
                .ToList();

            foreach (var consumerDoAssembly in consumersDoAssembly)
            {
                services.AddSingleton(consumerDoAssembly.AsType());
            }
        }

        private static IServiceCollection RegistrarApplication(this IServiceCollection services, CoreSettings settings)
        {
            if (settings.ConfigurarMediator)
            {
                services.AddMediatR(settings.TipoDoStartup);

                services.AddScoped<Application.Mediator.IMediator, Application.Mediator.Mediator>();

                services.RegistrarMediatorHandlers(settings);

                if (settings.ConfigurarExceptionPipelineBehavior)
                {
                    services.RegistrarExceptionPipelineBehavior();
                }

                services.RegistrarLogPipelineBehavior();

                if (settings.ConfigurarFailFastPipelineBehavior)
                {
                    services.RegistrarFailFastPipeline(settings);
                }
            }

            if (settings.ConfigurarKafkaBroker)
            {
                services.AddSingleton<IKafkaBroker, KafkaBroker>();
            }

            return services;
        }

        private static void RegistrarMediatorHandlers(this IServiceCollection services, CoreSettings settings)
        {
            var assembly = settings.TipoDaCamadaDeApplication.Assembly;

            var classesDaApplication = assembly.ExportedTypes
                .Select(t => t.GetTypeInfo())
                .Where(t => t.IsClass && !t.IsAbstract);

            foreach (var tipo in classesDaApplication)
            {
                var nomeDasInterfacesHandlers = new List<string>
                {
                    typeof(IRequestHandler<,>).Name,
                    typeof(INotificationHandler<>).Name
                };

                var interfacesDosHandlers = tipo.ImplementedInterfaces
                    .Select(i => i.GetTypeInfo())
                    .Where(i => nomeDasInterfacesHandlers.Contains(i.Name));

                foreach (var interfaceDoHandler in interfacesDosHandlers)
                {
                    services.AddTransient(interfaceDoHandler.AsType(), tipo.AsType());
                }
            }
        }

        private static void RegistrarExceptionPipelineBehavior(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));
        }

        private static void RegistrarLogPipelineBehavior(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogPipelineBehavior<,>));
        }

        private static void RegistrarFailFastPipeline(this IServiceCollection services, CoreSettings settings)
        {
            services.RegistrarValidators(settings);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastPipelineBehavior<,>));
        }

        private static void RegistrarValidators(this IServiceCollection services, CoreSettings settings)
        {
            var assembly = settings.TipoDaCamadaDeApplication.Assembly;

            foreach (var validator in AssemblyScanner.FindValidatorsInAssembly(assembly))
            {
                services.AddScoped(validator.InterfaceType, validator.ValidatorType);
            }
        }

        private static IServiceCollection RegistrarCrossCutting(this IServiceCollection services, CoreSettings settings)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            if (settings.ConfigureIEnvironment)
            {
                services.AddSingleton<IEnvironment, Environment>();
            }

            return services;
        }
    }
}
