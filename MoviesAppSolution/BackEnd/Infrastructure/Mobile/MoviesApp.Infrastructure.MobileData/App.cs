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
using System.IO;

namespace MoviesApp.Infrastructure.MobileData
{
    [Application]
    public class App : Application
    {
        public static string DirectoryPath =
            Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/MoviesApp";

        public static bool Created { get; private set; }

        static App()
        {
            ConfigureAppDirectory();
        }

        protected App(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Created = true;
        }

        private static void ConfigureAppDirectory()
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
        }

        public static T ResolveFactory<T>() where T : IServiceFactory
        {
            var factory = DependencyResolver.Resolve<T>();
            return factory;
        }
    }
}