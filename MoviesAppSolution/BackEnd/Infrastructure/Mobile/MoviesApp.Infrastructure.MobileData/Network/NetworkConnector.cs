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
using Android.Net;

namespace MoviesApp.Infrastructure.MobileData.Network
{
    public class NetworkConnector
    {
        public static bool HasConnection()
        {
            var conn = ConnectivityManager.FromContext(Application.Context);
            return conn.ActiveNetworkInfo?.IsConnectedOrConnecting ?? false;
        }

        public static ConnectivityType GetConnectionType()
        {
            var conn = ConnectivityManager.FromContext(Application.Context);
            return conn.ActiveNetworkInfo?.Type ?? ConnectivityType.Dummy;
        }
    }
}