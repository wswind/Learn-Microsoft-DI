using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace MicrosoftDI.Sample
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterByAssembly(
            this IServiceCollection services, string endString, 
            ServiceLifetime lifetime, bool optional, params Assembly[] assemblies)
        {
            Check.AssertNotNull(services, nameof(services));
            Check.AssertNotEmpty(endString, nameof(endString));
            Check.AssertNotNull(assemblies, nameof(assemblies));

            var alltypes = assemblies.SelectMany(x => x.DefinedTypes).Select(x => x.AsType()).Distinct();
            var implTypes = alltypes.Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith(endString));
            if(!implTypes.Any() && optional == false)
            {
                throw new InvalidOperationException($"Can't find classes endswith {endString}.");
            }

            foreach (var implType in implTypes)
            {
                var className = implType.Name;
                var servType = implType.GetInterfaces().Where(x => x.Name == $"I{className}").FirstOrDefault();

                if (optional == false && servType == null)
                {
                    throw new InvalidOperationException($"Can't find interface I{className} for class {servType.FullName}");
                }
                if (servType != null)
                {
                    services.TryAdd(new ServiceDescriptor(servType, implType, lifetime));
                }
            }

            return services;
        }
    }
}

