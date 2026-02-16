using EngineeringSystem.Backend.Api.Diagnostics.Abstractions;
using EngineeringSystem.Backend.Api.Diagnostics.Checks;
using EngineeringSystem.Backend.Api.Diagnostics.Registration;
using EngineeringSystem.Backend.Api.Diagnostics.Runtime;
using EngineeringSystem.Backend.Api.Options;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringSystem.Backend.Api
{
    public static class DependencyInjection
    { /// <summary>
        public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<IStartupCheckRunner, StartupCheckRunner>();                                        
            services.AddStartupChecksFromAssembly(typeof(DependencyInjection).Assembly);                                             
            services.Configure<DevDiagnosticsOptions>(configuration.GetSection(DevDiagnosticsOptions.SectionName));                                     
               


            return services;

        }
     }
}
