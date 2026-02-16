namespace EngineeringSystem.Backend.Api.Contracts.Orders
{
    public class CreateOrderResponse
    {
        public Guid OrderId { get; init; }
        public string Status { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}
