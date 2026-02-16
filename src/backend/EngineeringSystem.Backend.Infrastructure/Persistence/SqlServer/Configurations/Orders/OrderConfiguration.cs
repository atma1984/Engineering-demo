using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Orders;               
using EngineeringSystem.Backend.Domain.Orders.Contacts;      
using EngineeringSystem.Backend.Domain.Orders.Items;         
using Microsoft.EntityFrameworkCore;                         
using Microsoft.EntityFrameworkCore.Metadata.Builders;       

namespace EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.Configurations.Orders
{

    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedNever();
            builder.Property(o => o.CustomerId)
                .IsRequired();
            builder.Property(o => o.CreatedAt)
                .IsRequired();
            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<int>();
            builder.Navigation(o => o.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(o => o.Contacts)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.OwnsMany<OrderItem>(
                navigationName: nameof(Order.Items),
                item =>
                {
                    item.ToTable("OrderItems");
                    item.WithOwner().HasForeignKey("OrderId");    
                    item.HasKey(i => i.Id);
                    item.Property(i => i.Id).ValueGeneratedNever();                      
                    item.Property(i => i.Description)
                        .IsRequired()
                        .HasMaxLength(2000);
                    item.Property(i => i.NomenclatureItemId)
                        .IsRequired(false);
                    item.Property(i => i.Quantity)
                        .IsRequired(false);
                    item.Property(i => i.IsDefined)
                        .IsRequired();
                    item.HasIndex("OrderId"); //Индекс по OrderId для скорости выборок
                });

            builder.OwnsMany<OrderContact>(
                navigationName: nameof(Order.Contacts),
                contact =>
                {
                    contact.ToTable("OrderContacts");
                    contact.WithOwner().HasForeignKey("OrderId");
                    contact.Property<Guid>("Id");
                    contact.HasKey("Id");
                    contact.Property(c => c.ContactId).IsRequired();
                    contact.Property(c => c.Role)
                        .IsRequired()
                        .HasMaxLength(200);
                    contact.HasIndex("OrderId", nameof(OrderContact.ContactId)).IsUnique(); });
         }
    }
}
