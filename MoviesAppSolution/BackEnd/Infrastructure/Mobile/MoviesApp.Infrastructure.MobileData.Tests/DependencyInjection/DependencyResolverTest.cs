using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnit.Framework;
using MoviesApp.Services.ServiceFactory;
using System.Reflection;
using MoviesApp.Infrastructure.MobileData.DependencyInjection;
using Autofac;

namespace MoviesApp.Infrastructure.MobileData.Tests.DependencyInjection
{
    [TestFixture]
    class DependencyResolverTest
    {
        class TestServiceFactory : IServiceFactory
        {

        }

        [Test]
        public void ShouldBuildContainer()
        {
            var implTypesAssembliesNamespace = new Dictionary<Assembly, string[]>
                {
                    {
                        Assembly.GetExecutingAssembly(), new[] 
                        {
                            "MoviesApp.Infrastructure.MobileData.Tests.DependencyInjection"
                        }
                    }
                };

            var container = DependencyResolver.BuildContainer(implTypesAssembliesNamespace);
            Assert.IsNotNull(container);

            var serviceFactoryImpl = container.Resolve<IServiceFactory>();
            Assert.IsNotNull(serviceFactoryImpl);
            Assert.IsInstanceOfType(typeof(TestServiceFactory), serviceFactoryImpl);
        }
    }
}