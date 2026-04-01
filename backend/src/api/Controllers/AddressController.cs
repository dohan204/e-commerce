using System.Net.Mime;
using application.cases.Commands.Addresses;
using application.cases.Queries.Addresses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AddressController : ControllerBase
    {   
        private readonly IMediator _mediator;
        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Create addres success"
            });
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAddress(Guid userId)
        {
            var query = new GetAddressUserQuery { UserId = userId};
            var addres = await _mediator.Send(query);
            return Ok(addres);
        }
    }
}