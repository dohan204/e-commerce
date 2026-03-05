using application.cases.Commands.Categories;
using application.cases.Queries.Categories;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CreateCategoryHandler _handler; 
        public CategoryController(IMediator mediator, CreateCategoryHandler handler)
        {
            _mediator = mediator;
            _handler = handler;
        }
        [HttpGet("alls")] 
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            if(categories.Count == 0)
                return Ok(new
                {
                    message = "List empty category",
                    data = categories
                });
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(int id)
        {
            var categoryId = new GetCategoryQuery {Id = id};
            var category = await _mediator.Send(categoryId);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Created([FromBody] CreateCategoryCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new
            {
                Status = StatusCodes.Status201Created,
                Message = "Created Successfully"
            });
        }
        [HttpPut]
        public async Task<IActionResult> Updated([FromBody] UpdateCategoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Deleted([FromQuery] int Id)
        {
            var command = new DeleteCategoryCommand {CategoryId = Id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}