using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringSystem.Backend.Application.Orders.CreateOrder
{
    public sealed class CreateOrderResult
    {
        public Guid OrderId { get; init; }
        public string Status { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}
