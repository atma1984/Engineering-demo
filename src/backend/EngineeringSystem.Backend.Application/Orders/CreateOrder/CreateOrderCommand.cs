using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringSystem.Backend.Application.Orders.CreateOrder
{
    public class CreateOrderCommand
    {
        public Guid CustomerId { get; init; }

        public IReadOnlyCollection<CreateOrderItemDto> Items { get; init; }
            = Array.Empty<CreateOrderItemDto>();

        public IReadOnlyCollection<CreateOrderContactDto> Contacts { get; init; }
            = Array.Empty<CreateOrderContactDto>();

        public string? Comment { get; init; }

    }
    public sealed class CreateOrderItemDto
    {
        public string Description { get; init; } = string.Empty;
        public int? Quantity { get; init; }
    }
    public sealed class CreateOrderContactDto
    {
        public Guid ContactId { get; init; }
        public string Role { get; init; } = string.Empty;
    }
}
