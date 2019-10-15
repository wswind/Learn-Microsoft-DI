using Microsoft.Extensions.DependencyInjection;
using MicrosoftDIStudy.GenericServices;

namespace MicrosoftDIStudy
{
    public class DIManagerForGeneric : DIManagerBase
    {
        protected override void ConfigureServices()
        {
            _serviceCollection.AddTransient(typeof(IGenericServices<>),typeof(GenericService<>));
        }
    }
}
