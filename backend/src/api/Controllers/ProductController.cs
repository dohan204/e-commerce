using System.Net.Mime;
using api.Helpers.Dtos;
using application.cases.Commands.Product;
using application.cases.Dtos;
using application.cases.Queries.Products;
using application.interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            var query = new GetProductCommand { Id = id };
            var product = await _mediator.Send(query);
            return Ok(product);
        }
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        // [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command, IFormFile file)
        {

            command.ImageUrl = file.OpenReadStream();
            command.FileName = file.FileName;
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new ActionResponse
            {
                Status = StatusCodes.Status201Created,
                Message = "Create Product successfully."
            });
        }

        [HttpPut]
        // [Authorize]
        [Consumes("multipart/form-data")]
        // chỉ nhận dữ liệu kiểu json
        [ProducesResponseType(StatusCodes.Status204NoContent)] // trả về status 204
        public async Task<IActionResult> Update([FromForm] UpdateProductCommand command, IFormFile file)
        {

            // Kiểm tra xem file có thực sự tới được Controller không
            if (file == null || file.Length == 0)
                return BadRequest("File gửi lên bị null rồi bạn ơi!");

            // TẠO OBJECT MỚI HOẶC GÁN VÀO COMMAND HIỆN TẠI
            command.ImageUrl = file.OpenReadStream(); // DÒNG QUAN TRỌNG NHẤT
            command.FileName = file.FileName;

            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut("{id}/image")]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {

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
        [HttpDelete("{id}")]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("dataoverview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllData()
        {
            var data = await _mediator.Send(new GetProductDataDashboardQuery { });
            return Ok(new ApiResponse<object?>
            {
                Message = "Lấy dữ liệu thành công",
                Data = data
            });
        }


        [HttpGet("topSales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopSale()
        {
            var data = await _mediator.Send(new GetTopSaleProductQuery());
            if (!data.Any())
                return Ok(new ApiResponse<IEnumerable<TopProductSale>>
                {
                    Message = "Không có dữ liệu",
                    Data = data
                });
            return Ok(new ApiResponse<IEnumerable<TopProductSale>>
            {
                Message = "Lấy dữ liệu thành công",
                Data = data
            });
        }

        [HttpGet("pagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Pagination(int page, int pageSize, string? search, int? categoryId)
        {
            var query = new GetPaginationQuery { Page = page, PageSize = pageSize, Search = search, CategoryId = categoryId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] string search)
        {
            var query = new GetSearchProductQuery { Search = search};
            var result = await _mediator.Send(query);
            if(!result.Any())
                return Ok(new ApiResponse<IEnumerable<ProductViewDto>>
                {
                    Message = "Khong co du lieu",
                    Data = result
                });
            return Ok(new ApiResponse<IEnumerable<ProductViewDto>>
            {
                Message = "lấy du liêu thành công",
                Data = result
            });
        }

        [HttpGet("sales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Sales()
        {
            var data = await _mediator.Send(new GetSales());
           if(!data.Any())
                return Ok(new ApiResponse<IEnumerable<ProductViewDto>>
                {
                    Message = "Khong co du lieu",
                    Data = data
                });
            return Ok(new ApiResponse<IEnumerable<ProductViewDto>>
            {
                Message = "lấy du liêu thành công",
                Data = data
            });
        }
    }
}