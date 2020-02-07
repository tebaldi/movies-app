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
using MoviesApp.Infrastructure.MobileData.Network;
using Android.Net;

namespace MoviesApp.Infrastructure.MobileData.Tests.Network
{
    [TestFixture]
    class NetworkConnectorTest
    {
        [Test]
        public void ShouldCheckConnection()
        {
            var hasConnection = NetworkConnector.HasConnection();
            Assert.IsTrue(hasConnection);
        }

        [Test]
        public void ShouldValidateConnectionType()
        {
            var connection = NetworkConnector.GetConnectionType();
            Assert.AreNotEqual(ConnectivityType.Dummy, connection);
        }
    }
}