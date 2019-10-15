using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy
{
    public class DIManagerBase
    {
        protected ServiceCollection _serviceCollection = null;
        protected ServiceProvider _serviceProvider = null;
        public DIManagerBase()
        {
            _serviceCollection = new ServiceCollection();
            ConfigureServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        protected virtual void ConfigureServices()
        {

        }
     
        public T For<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }


}
