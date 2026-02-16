using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringSystem.Backend.Application.Orders.AssignOrderContact
{
    public sealed class AssignOrderContactResult
    {
        public Guid OrderId { get; init; }
        public Guid ContactId { get; init; }
        public string Role { get; init; } = string.Empty;
    }
}
