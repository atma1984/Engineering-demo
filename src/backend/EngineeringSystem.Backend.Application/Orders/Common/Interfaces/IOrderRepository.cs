using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Orders;

namespace EngineeringSystem.Backend.Application.Orders.Common.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order? GetById(Guid orderId);
        void Update(Order order);
    }
}
