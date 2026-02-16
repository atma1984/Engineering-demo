using EngineeringSystem.Backend.Api.Contracts.Orders;
using EngineeringSystem.Backend.Application.Orders.AssignOrderContact;
using EngineeringSystem.Backend.Application.Orders.CreateOrder;

namespace EngineeringSystem.Backend.Api.Mapping
{
    public class OrdersMapping
    {
        public static CreateOrderCommand ToCreateOrderCommand(CreateOrderRequest request)
        {
            return new CreateOrderCommand
            {
                CustomerId = request.CustomerId,
                Items = request.Items
                 .Select(i => new EngineeringSystem.Backend.Application.Orders.CreateOrder.CreateOrderItemDto
                 {
                     Description = i.Description,
                     Quantity = i.Quantity,
                     
                 })
                 .ToArray(),

                Contacts = request.Contacts
                 .Select(c => new CreateOrderContactDto
                 {
                     ContactId = c.ContactId,
                     Role = c.Role
                 })
                 .ToArray(),

                Comment = request.Comment
            };
        }
        public static CreateOrderResponse ToCreateOrderResponse(CreateOrderResult result)
        {
            return new CreateOrderResponse
            {
                OrderId = result.OrderId,
                Status = result.Status,
                CreatedAt = result.CreatedAt
            };
        }
        public static AssignOrderContactCommand ToAssignOrderContactCommand(
            Guid orderId,
            AssignOrderContactRequest request)
        {
            return new AssignOrderContactCommand
            {
                OrderId = orderId,
                ContactId = request.ContactId,
                Role = request.Role
            };
        }
        public static AssignOrderContactResponse ToAssignOrderContactResponse(AssignOrderContactResult result)
        {
            return new AssignOrderContactResponse
            {
                OrderId = result.OrderId,
                ContactId = result.ContactId,
                Role = result.Role
            };
        }
    }
}
