using api.Helpers.Dtos;
using application.cases.Commands.Categories;
using application.cases.Dtos;
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
        [ProducesResponseType(typeof(IReadOnlyList<Category>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GcetAll()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            if (categories.Count == 0)
                return Ok(new ApiResponse<IReadOnlyList<CategoryViewDto>>
                {
                    Message = "List empty category",
                    Data = categories
                });
            return Ok(new ApiResponse<IReadOnlyList<CategoryViewDto>>
            {
                Message = "Get list category successfully",
                Data = categories
            });
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByid(int id)
        {
            var categoryId = new GetCategoryQuery { Id = id };
            var category = await _mediator.Send(categoryId);
            return Ok(category);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(CreateCategoryCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateCategoryCommand), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Created([FromForm] CreateCategoryCommand command, IFormFile? file)
        {
            if (file.Length == 0 || file.Length == null)
            {
                return BadRequest("File Không hợp lệ: ");
            }

            command.Image = file.OpenReadStream();
            command.FileName = file.FileName;
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new
            {
                Status = StatusCodes.Status201Created,
                Message = "Created Successfully"
            });
        }
        [HttpPut]
        [ProducesResponseType(typeof(UpdateCategoryCommand), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Updated([FromBody] UpdateCategoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Deleted(int Id)
        {
            var command = new DeleteCategoryCommand { CategoryId = Id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}