using application.cases.Commands.Product;
using application.cases.Dtos;
using application.cases.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProductCommand {Id = id};
            var product = await _mediator.Send(query);
            return Ok(product);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetProductsQuery();
            var products = await _mediator.Send(query);
            if(!products.Any())
                return Ok(new
                {
                    message = "Chưa có sản phẩm nào",
                    data = products
                });
            return Ok(products);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new
            {
                status = StatusCodes.Status201Created,
                message = "Create Product successfully."
            });
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut("{id}/image")]
        [Authorize]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            if(file is null || file.Length < 0) 
                return BadRequest("No images providers");

            // chuyển iformfile thành stream 
            var command = new UpdateProductImageCommand
            {
                ProductId = id, 
                ImageUrl = file.OpenReadStream(),
                FileName = file.FileName
            };
            var imagePath = await _mediator.Send(command);
            return Ok(new { imagePath });
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}