using application.cases.Commands.Reviews;
using application.cases.Queries.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllReviewByProduct(int productId)
        {
            var reviews = await _mediator.Send(new GetReviewByProductQuery { ProductId = productId});
            return Ok(reviews);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReviewCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Created successfully."
            });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int reviewId)
        {
            await _mediator.Send(new DeleteReviewCommand { ReviewId = reviewId});
            return NoContent();
        }
    }
}