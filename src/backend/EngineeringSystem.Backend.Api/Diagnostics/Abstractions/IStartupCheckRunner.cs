using EngineeringSystem.Backend.Api.Diagnostics.Models;

namespace EngineeringSystem.Backend.Api.Diagnostics.Abstractions
{
    public interface IStartupCheckRunner
    {
        Task<IReadOnlyList<StartupCheckResult>> RunAllAsync(                                 
            IServiceProvider sp,                                                             
            CancellationToken ct = default);                                                
    }
}
