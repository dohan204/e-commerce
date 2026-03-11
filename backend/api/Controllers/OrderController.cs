using System.Net.Mime;
using application.cases.Commands.Orders;
using application.cases.Queries.Orders;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderHandler _handler;
        private readonly IMediator _mediator;
        public OrderController(CreateOrderHandler handler, IMediator mediator)
        {
            _handler = handler;
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [ProducesResponseType<Order>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _mediator.Send(new GetOrderQuery {Id = id});
            return Ok(order);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            if(!orders.Any())
                return Ok(new
                {
                    message = "list empty",
                    data = orders
                });
            return Ok(orders);
        }
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Created([FromBody] CreateOrderCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new
            {
                status = 201,
                message = "Created successfuly"
            });
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteOrderCommand { Id = id};
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}