using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Application.Orders.Common.Interfaces;
using EngineeringSystem.Backend.Domain.Common;

namespace EngineeringSystem.Backend.Application.Orders.AssignOrderContact
{
    public sealed class AssignOrderContactHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignOrderContactHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssignOrderContactResult> Handle(AssignOrderContactCommand command, CancellationToken ct = default)
        {
            // 1) Находим заказ
            var order = _orderRepository.GetById(command.OrderId);

            if (order is null)
                throw new DomainException($"Заказ с Id={command.OrderId} не найден.");

            // 2) Добавляем контакт (все правила внутри домена)
            order.AddContact(command.ContactId, command.Role);

            // 3) Сохраняем изменения
            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync(ct);

            // 4) Возвращаем результат
            return new AssignOrderContactResult
            {
                OrderId = command.OrderId,
                ContactId = command.ContactId,
                Role = command.Role
            };
        }
    }
}
