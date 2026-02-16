using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Customers;
using EngineeringSystem.Backend.Domain.Orders;
using EngineeringSystem.Backend.Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.DbContexts
{
    public sealed class CrmDbContext : DbContext
    {
        public CrmDbContext(DbContextOptions<CrmDbContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Customer> Customers => Set<Customer>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrmDbContext).Assembly);
        }
    }
}
