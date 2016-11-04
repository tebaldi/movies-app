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
using Autofac;
using MoviesApp.Infrastructure.MobileData.DependencyInjection;
using MoviesApp.Services.ServiceFactory;

namespace MoviesApp.Infrastructure.MobileData
{
    public class App : Application
    {
        static App()
        {
            
        }

        public static T ResolveFactory<T>() where T : IServiceFactory
        {
            var factory = DependencyResolver.Resolve<T>();
            return factory;
        }

        protected App(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}