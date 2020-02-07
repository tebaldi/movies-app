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
using System.Reflection;
using MoviesApp.Services.ServiceFactory;

namespace MoviesApp.Infrastructure.MobileData.DependencyInjection
{
    internal class DependencyResolver
    {
        private static IContainer container;

        public static T Resolve<T>() where T : IServiceFactory
        {
            if (container == null)
            {
                container = BuildContainer(new[]
                {
                    Assembly.Load("MoviesApp.Infrastructure.TMDb")
                });
            }

            return container.Resolve<T>();
        }

        public static IContainer BuildContainer(Assembly[] serviceFactoryAssemblies)
        {
            var builder = new ContainerBuilder();

            foreach (var assembly in serviceFactoryAssemblies)
            {
                var concreteTypes = assembly.GetTypes()
                    .Where(t => t != null && t.Name.EndsWith("ServiceFactory"));

                foreach (var type in concreteTypes)
                {
                    foreach (var interfaceType in type.GetInterfaces())
                    {
                        if (type.IsGenericTypeDefinition)
                        {
                            builder.RegisterGeneric(type).As(interfaceType)
                                .InstancePerDependency();
                        }
                        else
                        {
                            builder.RegisterType(type).As(interfaceType)
                                .InstancePerDependency();
                        }
                    }
                }
            }

            var container = builder.Build();
            return container;
        }
    }
}