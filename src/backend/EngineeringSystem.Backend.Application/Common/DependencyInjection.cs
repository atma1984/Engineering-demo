using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Application.Orders.CreateOrder;
using EngineeringSystem.Backend.Application.Orders.AssignOrderContact;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace EngineeringSystem.Backend.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var applicationServices = assembly
                .GetTypes()
                .Where(type =>
                    type is { IsClass: true, IsAbstract: false } &&
                    typeof(IApplicationService).IsAssignableFrom(type));

            foreach (var serviceType in applicationServices)
            {
                services.AddScoped(serviceType);
            }

            return services;
        }
    }
}
