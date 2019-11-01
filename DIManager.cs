using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicrosoftDI.Sample
{
    public class DIManager
    {
        private ServiceCollection _serviceCollection = null;
        private ServiceProvider _serviceProvider = null;
        private readonly Action<ServiceCollection> _configureServices;

        public DIManager(Action<ServiceCollection> configureServices)
        {
            _serviceCollection = new ServiceCollection();
            _configureServices(_serviceCollection);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _configureServices = configureServices;
        }

        public T For<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }


}
