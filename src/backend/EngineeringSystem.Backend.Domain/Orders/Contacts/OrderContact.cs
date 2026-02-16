using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;

namespace EngineeringSystem.Backend.Domain.Orders.Contacts
{
    public sealed class OrderContact
    {
        public Guid ContactId { get; }
        public OrderContactRole Role { get; }

        private OrderContact() { }

        public OrderContact(Guid contactId, string role)
        {
            if (contactId == Guid.Empty)
                throw new DomainException("Контакт заказа должен быть указан.");

            if (!Enum.TryParse<OrderContactRole>(role, ignoreCase: true, out var parsedRole))
                throw new DomainException($"Недопустимая роль контакта: {role}");

            ContactId = contactId;
            Role = parsedRole;
        }
    }
}
