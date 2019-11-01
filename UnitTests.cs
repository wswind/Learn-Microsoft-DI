using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MicrosoftDI.Sample;
using MicrosoftDI.Sample.GenericServices;
using MicrosoftDI.Sample.Services;
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
            DIManager dIManager = new DIManager(sc =>
            {
                sc.AddTransient<IService, Service>();
            });
            var serv = dIManager.For<IService>();
            int sum = serv.Sum(1, 2);
            Assert.Equal(3, sum);
        }

        [Fact]
        public void Can_Scan_Assembly_Ends_With_Service()
        {
            DIManager dIManager = new DIManager( sc=> {
            var assembly = Assembly.GetExecutingAssembly();
            ScanAssemblyEndsService(sc,assembly);
            });
            var serv = dIManager.For<IService>();
            Assert.True(serv is Service);
            int sum = serv.Sum(1, 2);
            Assert.Equal(3, sum);
        }
        private void ScanAssemblyEndsService(ServiceCollection sc, params Assembly[] assemblies)
        {
            var alltypes = assemblies.SelectMany(x => x.DefinedTypes).Select(x => x.AsType());
            var implTypes = alltypes.Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("Service"));
            foreach (var implType in implTypes)
            {
                var className = implType.Name;
                var servType = implType.GetInterfaces().Where(x => x.Name == $"I{className}").FirstOrDefault();
                if (servType != null)
                {
                    sc.TryAddTransient(servType, implType);
                }
            }
        }
        [Fact]
        public void Can_Register_Generic_Typs()
        {
            var dIManager = new DIManager(sc =>
            {
                sc.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            });
            var serv = dIManager.For<IGenericService<int>>();
            Assert.True(serv is GenericService<int>);
            bool equal = serv.Equal(3, 3);
            Assert.True(equal);
        }

        [Fact]
        public void Can_Register_Generic_Interface()
        {
            var dIManager = new DIManager(sc=> {
                sc.AddTransient(typeof(IGenericService<int>), typeof(GenericService2));
                sc.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));

            });
            var serv = dIManager.For<IGenericService<int>>();
            Assert.True(serv is GenericService2);
            bool equal = serv.Equal(3, 3);
            Assert.True(equal);

            var serv2 = dIManager.For<IGenericService<float>>();
            Assert.True(serv2 is GenericService<float>);
            equal = serv2.Equal((float)3.0 ,(float)3.0);
            Assert.True(equal);
        }
        [Fact]
        public void Can_Resolve_Generic_Caller()
        {
            var dIManager = new DIManager(sc=> {
                sc.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
                sc.AddTransient(typeof(IGenericService<int>), typeof(GenericService2));
                sc.AddTransient(typeof(GenericCaller<>));
            });
            var serv = dIManager.For<GenericCaller<int>>();
            Assert.True(serv.Serv is GenericService2);
            Assert.True(serv.Equal(3, 3));

        }
    }
}
