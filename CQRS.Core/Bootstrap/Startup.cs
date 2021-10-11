using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRS.Core.Application;
using CQRS.Core.Application.Kafka;
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
                .RegistrarApplication(settings)
                .RegistrarCrossCutting(settings);

            return services;
        }

        private static IServiceCollection RegistrarApplication(this IServiceCollection services, CoreSettings settings)
        {
            if (settings.ConfigurarMediator)
            {
                services.AddMediatR(settings.TipoDoStartup);

                services.AddScoped<Application.IMediator, Application.Mediator>();

                services.RegistrarMediatorHandlers(settings.NomeDoAssemblyDoApplication());

                if (settings.ConfigurarFailFastPipelineBehavior)
                {
                    services.RegistrarFailFastPipeline(settings.NomeDoAssemblyDoApplication());
                }
            }

            if (settings.ConfigurarKafkaBroker)
            {
                services.AddSingleton<IKafkaBroker, KafkaBroker>();
            }

            return services;
        }

        private static void RegistrarMediatorHandlers(this IServiceCollection services, string applicationAssembly)
        {
            var assembly = AppDomain.CurrentDomain.Load(applicationAssembly);

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

        private static void RegistrarFailFastPipeline(this IServiceCollection services, string applicationAssembly)
        {
            services.RegistrarValidators(applicationAssembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
        }

        private static void RegistrarValidators(this IServiceCollection services, string nomeDoApplicationAssembly)
        {
            var assembly = AppDomain.CurrentDomain.Load(nomeDoApplicationAssembly);

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
