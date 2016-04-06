using System;
using System.Data.Entity;
using Autofac;
using QuickElastic.Core.DataProviders;
using QuickElastic.Core.Domain;
using QuickElastic.Data;
using Topshelf;
using Topshelf.Autofac;

namespace QuickElastic.SyncService
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = ConfigureAutofacContainer();

            HostFactory.Run(hc =>
            {
                hc.UseAutofacContainer(container);
                hc.Service<GeneratedDataSyncService>(sc =>
                {
                    sc.ConstructUsingAutofacContainer();

                    sc.WhenStarted(gdss =>
                    {
                        gdss.SyncUsers();

                        Console.WriteLine("Service Started");
                    });

                    sc.WhenStopped(gdss =>
                    {
                        gdss.StopSyncUsers();

                        Console.WriteLine("Service Stopped");
                    });
                });
            });
        }

        private static IContainer ConfigureAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<QuickElasticContext>().As<DbContext>();

            builder.RegisterType<UserDataProvider>().As<IDataProvider<User>>();
            builder.RegisterType<UserElasticIndexer>().As<IElasticIndexer<User>>();

            builder.RegisterType<GeneratedDataSyncService>();

            var container = builder.Build();

            return container;
        }
    }
}
