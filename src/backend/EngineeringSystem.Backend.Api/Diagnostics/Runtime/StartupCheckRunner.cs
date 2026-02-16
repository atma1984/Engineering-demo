using EngineeringSystem.Backend.Api.Diagnostics.Abstractions;
using EngineeringSystem.Backend.Api.Diagnostics.Models;
using EngineeringSystem.Backend.Api.Options;
using Microsoft.Extensions.Options;

namespace EngineeringSystem.Backend.Api.Diagnostics.Runtime
{
    public sealed class StartupCheckRunner : IStartupCheckRunner
    {
        private readonly IEnumerable<IStartupCheck> _checks;                                 // все проверки из DI
        private readonly DevDiagnosticsOptions _options;                                     // настройки диагностики (app.development)

        public StartupCheckRunner(                                                         
            IEnumerable<IStartupCheck> checks,                                             
            IOptions<DevDiagnosticsOptions> options)                                       
        {
            _checks = checks;                                                              
            _options = options.Value;                                                      
        }

        public async Task<IReadOnlyList<StartupCheckResult>> RunAllAsync(                  
            IServiceProvider sp,                                                           
            CancellationToken ct = default)                                                
        {
            var results = new List<StartupCheckResult>();                                  
            
            var orderedChecks = _checks.OrderBy(x => x.Sequence).ToList();                 
                          
            foreach (var check in orderedChecks)                                           
            {
                if (_options.DisabledChecks.Contains(check.Name, StringComparer.OrdinalIgnoreCase))
                {
                    results.Add(new StartupCheckResult(                               
                        Name: check.Name,
                        Sequence: check.Sequence,
                        IsCritical: check.IsCritical,
                        Success: true,
                        Error: null));

                    continue;                                                               
                }

                try
                {
                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);    
                    cts.CancelAfter(TimeSpan.FromSeconds(_options.TimeoutSeconds));         
                                             
                    await check.RunAsync(sp, cts.Token);                                    
                                           
                    results.Add(new StartupCheckResult(
                        Name: check.Name,
                        Sequence: check.Sequence,
                        IsCritical: check.IsCritical,
                        Success: true,
                        Error: null));
                }
                catch (Exception ex)
                {                                                 
                    results.Add(new StartupCheckResult(
                        Name: check.Name,
                        Sequence: check.Sequence,
                        IsCritical: check.IsCritical,
                        Success: false,
                        Error: ex.Message));
         
                    if (check.IsCritical)
                        break;
                }
            }

            return results;                                                                 
        }
    }
}
