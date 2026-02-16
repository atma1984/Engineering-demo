using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Application.Orders.Common.Interfaces;
using EngineeringSystem.Backend.Domain.Orders;
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.Repositories.Orders
{

    public class OrderRepository : IOrderRepository
    {
        private readonly CrmDbContext _db;

        public OrderRepository(CrmDbContext db)
        {
            _db = db;
        }

        public void Add(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public Order? GetById(Guid orderId)
        {
            return _db.Orders
            .Include(o => o.Items)
            .Include(o => o.Contacts)
            .FirstOrDefault(o => o.Id == orderId);
        }

        public void Update(Order order)
        {
            _db.Orders.Update(order);
            _db.SaveChanges();
        }
    }
}
