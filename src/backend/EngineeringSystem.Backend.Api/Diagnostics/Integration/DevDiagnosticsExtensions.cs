using EngineeringSystem.Backend.Api.Diagnostics.Abstractions;

namespace EngineeringSystem.Backend.Api.Diagnostics.Integration
{
    public static class DevDiagnosticsExtensions
    {
        public static async Task<WebApplication> UseDevDiagnosticsAsync(this WebApplication app)
        {
            // Проверки делаем только в dev.
            if (!app.Environment.IsDevelopment())
                return app;

            var runner = app.Services.GetRequiredService<IStartupCheckRunner>();

            var results = await runner.RunAllAsync(app.Services); 

            Console.WriteLine("=== Dev startup checks ===");                                
            foreach (var r in results.OrderBy(x => x.Sequence))
            {
                var status = r.Success ? "OK" : "FAIL";
                var critical = r.IsCritical ? "CRITICAL" : "NON-CRITICAL";
                Console.WriteLine($"[{status}] ({critical}) {r.Sequence} {r.Name} {r.Error}");
            }
            if (results.Any(r => !r.Success && r.IsCritical))                               
                throw new Exception("Critical dev startup check failed. See logs above."); 



            return app;
        }
        public static void MapDevDiagnosticsEndpoints(this WebApplication app)              // extension: app.MapDevDiagnosticsEndpoints()
        {
            // Только в Development.                                                         
            if (!app.Environment.IsDevelopment())                                          
                return;                                                                    

                                                    
            app.MapGet("/dev/diagnostics/startup-checks", async (                            
                IStartupCheckRunner runner,                                                  
                IServiceProvider sp,                                                         
                CancellationToken ct) =>                                                     
            {
                                                    
                var results = await runner.RunAllAsync(sp, ct);                              
                return Results.Ok(results.OrderBy(x => x.Sequence));                         
            });
        }
    }
}
