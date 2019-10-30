using Microsoft.Extensions.DependencyInjection;
using MicrosoftDIStudy.GenericServices;

namespace MicrosoftDIStudy
{
    public class DIManagerForGeneric : DIManagerBase
    {
        protected override void ConfigureServices()
        {
            _serviceCollection.AddTransient(typeof(IGenericService<>),typeof(GenericService<>));
        }
    }

    public class DIManagerForGenericInterface : DIManagerBase
    {
        protected override void ConfigureServices()
        {
            _serviceCollection.AddTransient(typeof(IGenericService<int>), typeof(GenericService2));
            _serviceCollection.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
        }
    }
}
