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
    [TestFixture(Category = "Dependecy Injection", Description = nameof(DependencyResolverTest))]
    class DependencyResolverTest
    {
        class TestServiceFactory : IServiceFactory
        {

        }

        [Test(Description = nameof(ShouldBuildContainer))]
        public void ShouldBuildContainer()
        {
            var container = DependencyResolver.BuildContainer(new[]
                {
                    Assembly.GetExecutingAssembly()
                });
            Assert.IsNotNull(container);

            var serviceFactoryImpl = container.Resolve<IServiceFactory>();
            Assert.IsNotNull(serviceFactoryImpl);
            Assert.IsInstanceOfType(typeof(TestServiceFactory), serviceFactoryImpl);
        }

        [Test(Description = nameof(ShouldConfigureContainerForMobileApp))]
        public void ShouldConfigureContainerForMobileApp()
        {
            var movieServiceFactory = DependencyResolver.Resolve<IMovieServiceFactory>();
            Assert.IsNotNull(movieServiceFactory);
        }
    }
}