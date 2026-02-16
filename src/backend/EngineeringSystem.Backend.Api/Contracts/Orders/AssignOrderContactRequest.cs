using System.ComponentModel.DataAnnotations;

namespace EngineeringSystem.Backend.Api.Contracts.Orders
{
    public class AssignOrderContactRequest
    {
        [Required]
        public Guid ContactId { get; init; }
        [Required]
        [MaxLength(50)]
        public string Role { get; init; } = string.Empty;
    }
}
