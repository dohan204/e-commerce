using application.cases.Commands.Carts;
using application.cases.Queries.Carts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        // private readonly CreateCartHandler cartHandler;
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet("{Userid}")]
        public async Task<IActionResult> GetByUser(Guid Userid)
        {
            var userCart = await _mediator.Send(new GetCartByUserQuery { Userid = Userid});
            return Ok(userCart);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCartCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new
            {
                status = 201,
                message = "Created successfully"
            });
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CreateCartCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid UserId, [FromQuery] int ProductId)
        {
            var command = new DeleteCartItemCommand {UserId = UserId, ProductId = ProductId};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}