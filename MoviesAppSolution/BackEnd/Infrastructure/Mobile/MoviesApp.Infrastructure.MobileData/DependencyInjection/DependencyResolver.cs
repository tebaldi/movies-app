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

namespace MoviesApp.Infrastructure.MobileData.DependencyInjection
{
    internal class DependencyResolver
    {
        private static IContainer container;

        public static T Resolve<T>()
        {
            if (container == null)
            {
                var implTypesAssembliesNamespace = new Dictionary<Assembly, string[]>
                {
                    { Assembly.Load("DietPlan.DomainModel"), new[] { "DietPlan.DomainModel.CommandHandlers" } },
                    { Assembly.Load("DietPlan.QueryModel"), new[] { "DietPlan.QueryModel.EventHandlers", "DietPlan.QueryModel.QueryServices.Implementation" } },
                    { Assembly.Load("FoodAdmin.Infrastructure"), new[] { "FoodAdmin.Infrastructure.Implementations" } },
                    { Assembly.GetExecutingAssembly(), new[] { "DietPlanning.API.DietPlan.Models.Implementation" } }
                };

                container = BuildContainer(implTypesAssembliesNamespace);
            }

            return container.Resolve<T>();
        }

        public static IContainer BuildContainer(
            Dictionary<Assembly, string[]> implTypesAssembliesNamespace)
        {
            var builder = new ContainerBuilder();

            foreach (var implTypesAssembly in implTypesAssembliesNamespace.Keys)
            {
                var implTypesNamespace = implTypesAssembliesNamespace[implTypesAssembly];
                var implTypes = implTypesAssembly.GetTypes()
                    .Where(t => t != null && t.Namespace != null &&
                        implTypesNamespace.Any(it => t.Namespace.Contains(it)));

                foreach (var type in implTypes)
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