using EngineeringSystem.Backend.Application.Orders.CreateOrder;
using System.ComponentModel.DataAnnotations;

namespace EngineeringSystem.Backend.Api.Contracts.Orders
{

        public sealed class CreateOrderRequest
        {
            [Required] 
            public Guid CustomerId { get; init; }
            public IReadOnlyCollection<CreateOrderItemDto> Items { get; init; }
                = Array.Empty<CreateOrderItemDto>();
            public IReadOnlyCollection<CreateOrderContactDto> Contacts { get; init; }
                = Array.Empty<CreateOrderContactDto>();
            public string? Comment { get; init; }
        }
        public sealed class CreateOrderItemRequestDto
    {
            [Required]
            [MaxLength(2000)]
            public string Description { get; init; } = string.Empty;
            public int? Quantity { get; init; }
            public Guid? NomenclatureItemId { get; init; }

        }
        public sealed class CreateOrderContactRequesDto
    {
            [Required]
            public Guid ContactId { get; init; }
            [Required]
            [MaxLength(50)]
            public string Role { get; init; } = string.Empty;
        }
    
}
