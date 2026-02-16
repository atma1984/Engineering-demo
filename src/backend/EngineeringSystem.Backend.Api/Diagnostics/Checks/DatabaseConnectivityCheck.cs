
using EngineeringSystem.Backend.Api.Diagnostics.Abstractions;
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts;

namespace EngineeringSystem.Backend.Api.Diagnostics.Checks
{

    public class DatabaseConnectivityCheck : IStartupCheck
    {


       
        public int Sequence => 100;                                                          
        public bool IsCritical => true;                                                   

        public async Task RunAsync(IServiceProvider sp, CancellationToken ct = default)
        {         
            using var scope = sp.CreateScope();                                     
            var db = scope.ServiceProvider.GetRequiredService<CrmDbContext>();
            var canConnect = await db.Database.CanConnectAsync(ct);
            Console.WriteLine($"[StartupCheck] DB connectivity: {(canConnect ? "OK" : "FAILED")}");

               throw new Exception("Cannot connect to database (CanConnectAsync returned false)."); 
        }
    }
}
