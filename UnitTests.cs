using Microsoft.Extensions.DependencyInjection;
using MicrosoftDIStudy.GenericServices;
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

        [Fact]
        public void Can_Register_Generic_Typs()
        {
            DIManagerForGeneric dIManager = new DIManagerForGeneric();
            var serv = dIManager.For<IGenericServices<int>>();
            bool equal = serv.Equals(3, 3);
            Assert.True(equal);
        }
    }
}
