using Microsoft.Extensions.DependencyInjection;
using MicrosoftDIStudy.Services;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace MicrosoftDIStudy
{
    public class UnitTests
    {
        [Fact]
        public void Can_Use_Simple_DI()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IService, Service>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var serv = serviceProvider.GetService<IService>();
            int sum = serv.Sum(1, 2);
            Assert.Equal(3, sum);
        }

        [Fact]
        public void Can_Scan_Assembly_Ends_With_Service()
        {
            DIManagerForScan dIManager = new DIManagerForScan();
            var serv = dIManager.For<IService>();
            int sum = serv.Sum(1, 2);
            Assert.Equal(3, sum);
        }

    }
}
