using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRS.Core.Application;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mediator = MediatR.Mediator;

namespace CQRS.Core.Bootstrap
{
    public static class Startup
    {
        public static IServiceCollection RegistrarCore(this IServiceCollection services, CoreSettings settings)
        {
            services
                .AddSingleton(x => settings)
                .RegistrarApplication(settings);

            return services;
        }

        private static IServiceCollection RegistrarApplication(this IServiceCollection services, CoreSettings settings)
        {
            services.AddScoped<Application.Interfaces.IMediator, Application.Mediator>();

            if (settings.ConfigurarMediator)
            {
                services.AddMediatR(settings.TipoDoStartup);
                services.AddScoped<IMediator, Mediator>();

                services.RegistrarMediatorHandlers(settings.NomeDoApplicationAssembly);

                if (settings.ConfigurarPipelineBehaviorDoMediator)
                {
                    services.RegistrarPipelineBehaviorsDoMediator(settings.NomeDoApplicationAssembly);
                }
            }

            return services;
        }

        private static IServiceCollection RegistrarMediatorHandlers(
            this IServiceCollection services,
            string nomeDoApplicationAssembly)
        {
            var assembly = AppDomain.CurrentDomain.Load(nomeDoApplicationAssembly);

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
            
            return services;
        }

        private static IServiceCollection RegistrarPipelineBehaviorsDoMediator(
            this IServiceCollection services,
            string nomeDoApplicationAssembly)
        {
            services.RegistrarValidators(nomeDoApplicationAssembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            return services;
        }

        private static void RegistrarValidators(this IServiceCollection services, string nomeDoApplicationAssembly)
        {
            var assembly = AppDomain.CurrentDomain.Load(nomeDoApplicationAssembly);

            foreach (var validator in AssemblyScanner.FindValidatorsInAssembly(assembly))
            {
                services.AddScoped(validator.InterfaceType, validator.ValidatorType);
            }
        }
    }
}
