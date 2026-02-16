using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Application.Orders.Common.Interfaces;
using EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts;

namespace EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.UnitOfWork
{
    public sealed class EfUnitOfWork : IUnitOfWork
        {
            private readonly CrmDbContext _db;

            public EfUnitOfWork(CrmDbContext db)
            {
                _db = db;
            }

            public Task<int> SaveChangesAsync(CancellationToken ct = default)
                => _db.SaveChangesAsync(ct);
        }
    
}
