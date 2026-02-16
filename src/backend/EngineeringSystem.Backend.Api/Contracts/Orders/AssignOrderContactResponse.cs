namespace EngineeringSystem.Backend.Api.Contracts.Orders
{
    public class AssignOrderContactResponse
    {
        public Guid OrderId { get; init; }
        public Guid ContactId { get; init; }
        public string Role { get; init; } = string.Empty;
    }
}
