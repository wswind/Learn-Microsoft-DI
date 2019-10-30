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
            Assert.True(serv is Service);
            int sum = serv.Sum(1, 2);
            Assert.Equal(3, sum);
        }

        [Fact]
        public void Can_Register_Generic_Typs()
        {
            var dIManager = new DIManagerForGeneric();
            var serv = dIManager.For<IGenericService<int>>();
            Assert.True(serv is GenericService<int>);
            bool equal = serv.Equal(3, 3);
            Assert.True(equal);
        }

        [Fact]
        public void Can_Register_Generic_Interface()
        {
            var dIManager = new DIManagerForGenericInterface();
            var serv = dIManager.For<IGenericService<int>>();
            Assert.True(serv is GenericService2);
            bool equal = serv.Equal(3, 3);
            Assert.True(equal);

            var serv2 = dIManager.For<IGenericService<float>>();
            Assert.True(serv2 is GenericService<float>);
            equal = serv2.Equal((float)3.0 ,(float)3.0);
            Assert.True(equal);
        }
    }
}
