using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Application.Common;
using EngineeringSystem.Backend.Application.Orders.Common.Interfaces;
using EngineeringSystem.Backend.Domain.Orders;

namespace EngineeringSystem.Backend.Application.Orders.CreateOrder
{
    public sealed class CreateOrderHandler : IApplicationService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken ct = default)
        {
            // 1. Создаём агрегат заказа через домен
            var order = Order.Create(command.CustomerId);

            // 2. Добавляем позиции заказа (если они есть)
            foreach (var item in command.Items)
            {
                var orderItem = order.AddItem(item.Description);

                // Количество — опционально
                if (item.Quantity.HasValue)
                {
                    orderItem.SetQuantity(item.Quantity.Value);
                }
            }

            // 3. Добавляем контакты (если они переданы)
            foreach (var contact in command.Contacts)
            {
                order.AddContact(
                    contact.ContactId,
                    contact.Role
                );
            }

            // 4. Сохраняем заказ
            _orderRepository.Add(order);

            await _unitOfWork.SaveChangesAsync(ct);

            // 5. Возвращаем результат сценария
            return new CreateOrderResult
            {
                OrderId = order.Id,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt
            };
        }
    } 
}
