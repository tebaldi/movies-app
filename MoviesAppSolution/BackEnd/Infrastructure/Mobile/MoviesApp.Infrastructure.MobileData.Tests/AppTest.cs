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

namespace MoviesApp.Infrastructure.MobileData.Tests
{
    [TestFixture]
    class AppTest
    {
        [Test]
        public void ShouldConfigureAppOnStartup()
        {
            Assert.IsTrue(App.Created);
            Assert.IsNotNull(App.DirectoryPath);
        }

        [Test]
        public void ShouldConfigureDependecyInjectionOnStartup()
        {
            var factory = App.ResolveFactory<IServiceFactory>();
            Assert.IsNotNull(factory);
        }
    }
}