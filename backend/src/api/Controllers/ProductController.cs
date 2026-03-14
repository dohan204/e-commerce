using System.Net.Mime;
using api.Helpers.Dtos;
using application.cases.Commands.Product;
using application.cases.Dtos;
using application.cases.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

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
        [ProducesResponseType<ProductViewDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProductCommand {Id = id};
            var product = await _mediator.Send(query);
            return Ok(product);
        }
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType<ProductViewDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(new ApiResponse<IEnumerable<ProductViewDto>>
            {
                Message = products.Any() ? "Lấy danh sách thành công" : "Chưa có sản phẩm nào!",
                Data = products
            });
        }
        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new ActionResponse
            {
                Status = StatusCodes.Status201Created,
                Message = "Create Product successfully."
            });
        }

        [HttpPut]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)] // chỉ nhận dữ liệu kiểu json
        [ProducesResponseType(StatusCodes.Status204NoContent)] // trả về status 204
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut("{id}/image")]
        [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            if(file is null || file.Length < 0) 
                return BadRequest("No images providers");
            string[] allowedExtension = new [] {".png", ".jpeg", ".png", ".webp"};
            var extenstionFile = Path.GetExtension(file.FileName).ToLower();

            if(!allowedExtension.Contains(extenstionFile))
                return BadRequest("Chỉ file có tên jpeg, png và webp là được cho phép");

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}