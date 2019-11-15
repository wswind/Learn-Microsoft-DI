using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace MicrosoftDI.Sample
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterByAssembly(this IServiceCollection services, string endString, ServiceLifetime lifetime, bool optionnal, params Assembly[] assemblies)
        {
            return services.RegisterByAssembly(new string[] { endString }, lifetime, optionnal, assemblies);
        }
        public static IServiceCollection RegisterByAssembly(this IServiceCollection services, string[] endString, ServiceLifetime lifetime, bool optionnal, params Assembly[] assemblies)
        {
            Check.AssertNotNull(services, nameof(services));
            Check.AssertNotEmpty(endString, nameof(endString));
            Check.AssertNotNull(assemblies, nameof(assemblies));

            var alltypes = assemblies.SelectMany(x => x.DefinedTypes).Select(x => x.AsType());
            var implTypes = alltypes.Where(x => x.IsClass && !x.IsAbstract && endString.Any(y => x.Name.EndsWith(y)));
            foreach (var implType in implTypes)
            {
                var className = implType.Name;
                var servType = implType.GetInterfaces().Where(x => x.Name == $"I{className}").FirstOrDefault();

                if (optionnal == false && servType == null)
                {
                    throw new InvalidOperationException($"Can't find interface I{className} for class {servType.FullName}");
                }

                if (servType != null)
                {
                    services.Add(new ServiceDescriptor(servType, implType, lifetime));
                }
            }

            return services;
        }
    }
}

