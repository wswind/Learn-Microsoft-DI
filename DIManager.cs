using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicrosoftDI.Sample
{
    public class diManager
    {
        private ServiceProvider _serviceProvider = null;
        
        public diManager(Action<ServiceCollection> configureServices)
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
