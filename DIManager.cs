using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicrosoftDI.Sample
{
    public class DiManager
    {
        private ServiceProvider _serviceProvider = null;
        
        public DiManager(Action<ServiceCollection> configureServices)
        {
            var serviceCollection = new ServiceCollection();
            configureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T For<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }


}
