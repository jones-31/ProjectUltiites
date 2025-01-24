using ProjectUtilities.DataAccess;
using ProjectUtilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProjectUtilities.Middlewares;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace ProjectUtilities
{
    public static class AddServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectUtilitiesServices(this IServiceCollection services)
        {


            services.AddSingleton<IDataConnections, DataConnections>();
            services.AddSingleton<ICustomLogger, CustomLogger>();
            services.AddScoped<IHashing, Hashing>();

            //services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());
            // USE AUTOFAC For more complex DI
            //services.AddSingleton<IDataConnections, DataConnections>();
            //services.AddSingleton<ICustomLogger, CustomLogger>();
            //services.AddScoped<IHashing, Hashing>();


            services.AddTransient<Func<string, ISQLDataAccess>>(provider => key =>
            {
                var dataconnections = provider.GetRequiredService<IDataConnections>();
                var customlogger = provider.GetRequiredService<ICustomLogger>();
                return new SQLDataAccess(dataconnections, key, customlogger);
            });

            services.AddTransient<Func<string, IDapperDataAccess>>(provider => key =>
            {
                var dataconnections = provider.GetRequiredService<IDataConnections>();
                var customlogger = provider.GetRequiredService<ICustomLogger>();
                return new DapperDataAccess(dataconnections, key, customlogger);
            });
            

            services.AddScoped<DALServices>();
            return services;

        }


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        // Host.CreateDefaultBuilder(args)
        //.ConfigureServices((context, services) =>
        //{
        //    // Register DALServices with DI
        //    services.AddScoped<DALServices>();
        //});

        public static void AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                                .Where(t => t.IsClass && !t.IsAbstract)
                                .SelectMany(t => t.GetInterfaces()
                                                  .Select(i => new { Interface = i, Implementation = t }));

            foreach (var type in types)
            {
                services.AddTransient(type.Interface, type.Implementation);
            }
        }
    }






}
