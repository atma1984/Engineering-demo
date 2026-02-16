namespace EngineeringSystem.Backend.Api.Options
{
    public sealed class DevDiagnosticsOptions
    {
        public const string SectionName = "DevDiagnostics";                                   // имя секции в конфиге

        public int TimeoutSeconds { get; init; } = 5;                                         // таймаут на одну проверку

        public string[] DisabledChecks { get; init; } = Array.Empty<string>();                // список отключенных проверок
    }
}
