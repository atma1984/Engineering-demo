using System.Reflection;
using EngineeringSystem.Backend.Api.Diagnostics.Abstractions;

namespace EngineeringSystem.Backend.Api.Diagnostics.Registration
{
    public static class StartupChecksRegistration
    {
        // extension method регистрации
        public static IServiceCollection AddStartupChecksFromAssembly(                       
            this IServiceCollection services,                                               
            Assembly assembly)                                                              
        {
            // Находим типы, которые являются классами и реализуют IStartupCheck(маркер).           
            var checkTypes = assembly
                .GetTypes()                                                                 
                .Where(t =>
                    !t.IsAbstract &&                                                        
                    !t.IsInterface &&                                                       
                    typeof(IStartupCheck).IsAssignableFrom(t));                             

                             
            foreach (var type in checkTypes)                                                
            {
                services.AddSingleton(typeof(IStartupCheck), type);                         
            }

            return services;                                                                
        }
    }
}
