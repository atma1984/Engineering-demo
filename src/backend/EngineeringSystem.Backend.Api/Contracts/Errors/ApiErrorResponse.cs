namespace EngineeringSystem.Backend.Api.Contracts.Errors
{
    public sealed class ApiErrorResponse
    {
        public string TraceId { get; init; } = string.Empty;                                  // идентификатор запроса (для поддержки)
        public string Error { get; init; } = string.Empty;                                    // сообщение для пользователя
    }
}