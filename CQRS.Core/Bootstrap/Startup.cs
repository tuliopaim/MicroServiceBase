using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRS.Core.API;
using CQRS.Core.API.Hateoas;
using CQRS.Core.Infrastructure.Kafka;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Core.Bootstrap
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

                services.AddScoped<Application.Mediator.Mediator.IMediator, Application.Mediator.Mediator>();

                services.RegistrarMediatorHandlers(settings);

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
            var assembly = settings.TipoDoApplicationMarker.Assembly;

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

        private static void RegistrarFailFastPipeline(this IServiceCollection services, CoreSettings settings)
        {
            services.RegistrarValidators(settings);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
        }

        private static void RegistrarValidators(this IServiceCollection services, CoreSettings settings)
        {
            var assembly = settings.TipoDoApplicationMarker.Assembly;

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
                if (settings.HostEnvironment == null)
                    throw new ArgumentException(nameof(settings.HostEnvironment));

                if (settings.Configuration == null)
                    throw new ArgumentException(nameof(settings.Configuration));

                var environmentName = settings.HostEnvironment.EnvironmentName;

                var parameters = settings
                    .Configuration
                    .AsEnumerable()
                    .ToDictionary(x => x.Key, x => x.Value);

                var environment = new Environment(environmentName, parameters);
                services.AddSingleton<IEnvironment>(environment);
            }

            return services;
        }
    }
}
