using ProjectUtilities.DataAccess;
using ProjectUtilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProjectUtilities.Middlewares;

namespace ProjectUtilities
{
    public static class AddServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectUtilitiesServices(this IServiceCollection services)
        {

            services.AddSingleton<IDataConnections, DataConnections>();
            services.AddSingleton<ICustomLogger, CustomLogger>();
            //services.AddTransient<CustomExceptionMiddleware>();

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

            return services;

        }
    }
}
