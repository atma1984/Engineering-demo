namespace EngineeringSystem.Backend.Api.Diagnostics.Abstractions
{
    public interface IStartupCheck
    {
        string Name => GetType().Name;    
        int Sequence => 0;                  
        bool IsCritical => true;       
        Task RunAsync(IServiceProvider sp, CancellationToken ct = default);  

    }
}
