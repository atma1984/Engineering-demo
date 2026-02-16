using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;
using EngineeringSystem.Backend.Domain.Orders.Contacts;
using EngineeringSystem.Backend.Domain.Orders.Items;
using EngineeringSystem.Backend.Domain.Orders.Notes;
using EngineeringSystem.Backend.Domain.Orders.Questions;

namespace EngineeringSystem.Backend.Domain.Orders
{
    public sealed class Order : AggregateRoot
    {
        private readonly List<OrderItem> _items = new();
        private readonly List<OrderContact> _contacts = new();

        internal Order(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new DomainException("Заказ не может быть создан без заказчика.");

            Id = Guid.NewGuid();
            CustomerId = customerId;
            Status = OrderStatus.Draft;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid CustomerId { get; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; }

        public IReadOnlyCollection<OrderItem> Items => _items;
        public IReadOnlyCollection<OrderContact> Contacts => _contacts;

        public static Order Create(Guid customerId)
        {
            return new Order(customerId);
        }

        public OrderItem AddItem(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Описание позиции заказа обязательно.");

            var item = new OrderItem(
        id: Guid.NewGuid(),
        description: description
    );

            _items.Add(item);

            return item;
        }

        public void AddContact(Guid contactId, string role)
        {
            if (contactId == Guid.Empty)
                throw new DomainException("Контакт обязателен.");

            if (_contacts.Any(c => c.ContactId == contactId))
                throw new DomainException("Контакт уже добавлен в заказ.");

            _contacts.Add(new OrderContact(contactId, role));
        }

        public void AddComment(string comment)
        {
            // пока просто задел, без реализации
        }
    }
}
