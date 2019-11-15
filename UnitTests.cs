using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MicrosoftDI.Sample.GenericServices;
using MicrosoftDI.Sample.Services;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace MicrosoftDI.Sample
{
    public class UnitTests
    {
        protected readonly ITestOutputHelper Output;
        public UnitTests(ITestOutputHelper  testOutputHelper)
        {
            Output = testOutputHelper;
        }


        [Fact]
        public void Can_Use_Simple_DI()
        {
            diManager diManager = new diManager(sc =>
            {
                sc.AddTransient<ISampleService, SampleService>();
            });
            var serv = diManager.For<ISampleService>();
            int sum = serv.Sum(1, 2);
            Assert.Equal(3, sum);
        }

        [Fact]
        public void Can_Scan_Assembly_Ends_With_Service()
        {
            diManager diManager = new diManager(sc =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                sc.RegisterByAssembly("SampleService", ServiceLifetime.Transient, false, assembly);
            });
            var serv = diManager.For<ISampleService>();
            Assert.True(serv is SampleService);
            int sum = serv.Sum(1, 2);
            Assert.Equal(3, sum);
        }
      
        [Fact]
        public void Can_Register_Generic_Typs()
        {
            var diManager = new diManager(sc =>
            {
                sc.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            });
            var serv = diManager.For<IGenericService<int>>();
            Assert.True(serv is GenericService<int>);
            bool equal = serv.Equal(3, 3);
            Assert.True(equal);
        }

        [Fact]
        public void Can_Register_Generic_Interface()
        {
            var diManager = new diManager(sc =>
            {
                sc.AddTransient(typeof(IGenericService<int>), typeof(ExplicitService));
                sc.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));

            });
            var serv = diManager.For<IGenericService<int>>();
            Assert.True(serv is ExplicitService);
            bool equal = serv.Equal(3, 3);
            Assert.True(equal);

            var serv2 = diManager.For<IGenericService<float>>();
            Assert.True(serv2 is GenericService<float>);
            equal = serv2.Equal((float)3.0, (float)3.0);
            Assert.True(equal);
        }
        [Fact]
        public void Can_Resolve_Generic_Caller()
        {
            var diManager = new diManager(sc =>
            {
                sc.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
                sc.AddTransient(typeof(IGenericService<int>), typeof(ExplicitService));
                sc.AddTransient(typeof(GenericCaller<>));
            });
            var serv = diManager.For<GenericCaller<int>>();
            Assert.True(serv.Serv is ExplicitService);
            Assert.True(serv.Equal(3, 3));

        }

        [Fact]
        public void Can_Not_Resolve_MoreType_Service()
        {
            var diManager = new diManager(sc =>
            {
                sc.AddTransient(typeof(IMoreInTypeService<>), typeof(MoreInTypeService<,>));
            });
            try
            {
                var serv = diManager.For<IMoreInTypeService<int>>();
            }
            catch(Exception ex)
            {
                Assert.True(ex is System.ArgumentException);
                Output.WriteLine(ex.Message);
            }
        }

        //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.0&tabs=visual-studio#consuming-a-scoped-service-in-a-background-task-1
        [Fact]
        public void Can_Use_Scope()
        {
            var diManager = new diManager(sc =>
            {
                sc.AddScoped<ISampleService, SampleService>();
            });
           
            var serv = diManager.For<ISampleService>();
            Assert.True(serv != null);
            Assert.True(serv is SampleService);

            IServiceProvider sp = diManager.For<IServiceProvider>();
            Assert.True(sp != null);

            ISampleService s1, s2;
            s1 = sp.GetService<ISampleService>();
            s2 = sp.GetService<ISampleService>();
            Assert.True(s1 == s2);

            using (var scope = sp.CreateScope())
            {
                s1 = scope.ServiceProvider.GetService<ISampleService>();
            }
            using (var scope = sp.CreateScope())
            {
                s2 = scope.ServiceProvider.GetService<ISampleService>();
            }

            Assert.True(s1 != s2);


            using (var scope = sp.CreateScope())
            {
                s1 = scope.ServiceProvider.GetService<ISampleService>();
                //different compare to autofac nested scope
                //https://docs.autofac.org/en/latest/lifetime/instance-scope.html#instance-per-matching-lifetime-scope
                using (var scope2 = scope.ServiceProvider.CreateScope())
                {
                    s2 = scope2.ServiceProvider.GetService<ISampleService>();
                }
                Assert.True(s1 != s2);
            }
           

            using (var scope = sp.CreateScope())
            {
                var s3 = scope.ServiceProvider.GetRequiredService<ISampleService>();
                var s4 = scope.ServiceProvider.GetRequiredService<ISampleService>();
                Assert.True(s3 == s4);
            }
        }
    }
}
