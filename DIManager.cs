using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Reflection;

namespace MicrosoftDIStudy
{
    public class DIManager : DIManagerBase
    {
        protected override void ConfigureServices()
        {
            var assembly = Assembly.GetExecutingAssembly();
            ScanAssemblyEndsService(assembly);
        }

        private void ScanAssemblyEndsService(params Assembly[] assemblies)
        {
            var alltypes = assemblies.SelectMany(x => x.DefinedTypes).Select(x=>x.AsType());
            var implTypes = alltypes.Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("Service"));
            foreach(var implType in implTypes)
            {
                var className = implType.Name;
                var servType = implType.GetInterfaces().Where(x=>x.Name == $"I{className}").FirstOrDefault();
                if(servType != null)
                {
                    _serviceCollection.TryAddTransient(servType, implType);
                }
            }
        }

    }
}
