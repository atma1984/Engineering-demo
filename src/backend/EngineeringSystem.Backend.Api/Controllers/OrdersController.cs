using EngineeringSystem.Backend.Api.Contracts.Orders;
using EngineeringSystem.Backend.Api.Mapping;
using EngineeringSystem.Backend.Application.Orders.AssignOrderContact;
using EngineeringSystem.Backend.Application.Orders.CreateOrder;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringSystem.Backend.Api.Controllers
{
    [ApiController] 
                    
                    

    [Route("api/orders")] 
    public class OrdersController : ControllerBase
    {
        // Handler сценария "создать заказ"
        private readonly CreateOrderHandler _createOrderHandler;

        // Handler сценария "назначить контакт в заказ"
        private readonly AssignOrderContactHandler _assignOrderContactHandler;
        
        public OrdersController(
            CreateOrderHandler createOrderHandler,
            AssignOrderContactHandler assignOrderContactHandler)
        {
            _createOrderHandler = createOrderHandler;
            _assignOrderContactHandler = assignOrderContactHandler;
        }
        [HttpPost] 
        [ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderRequest request, 
            CancellationToken ct)                  
        {
            
            if (request is null)
                return BadRequest("Тело запроса не может быть пустым.");

            if (request.CustomerId == Guid.Empty)
                return BadRequest("CustomerId обязателен.");

            
            CreateOrderCommand command = OrdersMapping.ToCreateOrderCommand(request);

            
            CreateOrderResult result = await _createOrderHandler.Handle(command, ct);

            
            CreateOrderResponse response = OrdersMapping.ToCreateOrderResponse(result);

            return Created(
                uri: $"api/orders/{response.OrderId}",
                value: response
            );
        }

        [HttpPost("{orderId:guid}/contacts")] 
        [ProducesResponseType(typeof(AssignOrderContactResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignContact(
            [FromRoute] Guid orderId,                    
            [FromBody] AssignOrderContactRequest request,
            CancellationToken ct)
        {
          
            if (orderId == Guid.Empty)
                return BadRequest("orderId в маршруте обязателен.");

            if (request is null)
                return BadRequest("Тело запроса не может быть пустым.");

            if (request.ContactId == Guid.Empty)
                return BadRequest("ContactId обязателен.");

           
            AssignOrderContactCommand command = OrdersMapping.ToAssignOrderContactCommand(orderId, request);

           
            AssignOrderContactResult result = await _assignOrderContactHandler.Handle(command, ct);

           
            AssignOrderContactResponse response = OrdersMapping.ToAssignOrderContactResponse(result);

           
            return Ok(response);

        }
        }
}
