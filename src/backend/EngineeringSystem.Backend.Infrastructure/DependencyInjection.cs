using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Application.Orders.Common.Interfaces;
using EngineeringSystem.Backend.Infrastructure.Persistence.Connection;
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts;
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.Repositories.Orders;
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EngineeringSystem.Backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton(_ => SqlServerConnectionOptions.From(configuration));
            services.AddSingleton<IDbConnectionStringProvider, SqlServerConnectionStringProvider>();


            services.AddDbContext<CrmDbContext>((sp, options) =>
            {
                var csProvider = sp.GetRequiredService<IDbConnectionStringProvider>();
                var connectionString = csProvider.GetConnectionString();

                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }
}
