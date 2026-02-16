namespace EngineeringSystem.Backend.Api.Diagnostics.Models
{
    public sealed record StartupCheckResult(                                                // record : модель результата
        string Name,                                                                        
        int Sequence,                                                                       
        bool IsCritical,                                                                    
        bool Success,                                                                       
        string? Error);
}
