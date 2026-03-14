using MediatR;
using Moq;



using api.Controllers;
using application.cases.Dtos;
using application.cases.Queries.Products;
using Microsoft.AspNetCore.Mvc;
using application.exceptions;
using Org.BouncyCastle.Crypto.Engines;
using api.Helpers.Dtos;
using application.cases.Commands.Product;
using Org.BouncyCastle.Tls;
namespace UnitTests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        private Mock<IMediator> _mediator;
        private ProductController _controller;
        public TestContext TestContext {get; set;}
        public static IEnumerable<ProductViewDto> GetProductTest()
        {
            yield return new ProductViewDto { Id = 1, Name = "Giày", Description = "Khong có", Price = 200, SalePrice = 0, AvgRating = 4, ImageUrl = string.Empty };
            yield return new ProductViewDto { Id = 2, Name = "dép", Description = "Khong có", Price = 250, SalePrice = 4, AvgRating = 45, ImageUrl = string.Empty };
            yield return new ProductViewDto { Id = 3, Name = "áo", Description = "Khong có", Price = 20, SalePrice = 0, AvgRating = 2, ImageUrl = string.Empty };
            yield return new ProductViewDto { Id = 4, Name = "quần", Description = "Khong có", Price = 145, SalePrice = 0, AvgRating = 4, ImageUrl = string.Empty };
        }
        [TestInitialize]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();

            // khởi tạo controller
            _controller = new ProductController(_mediator.Object);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnOk_WithProductData()
        {
            // arrage: chuẩn bị dữ liệu để cho vào test
            int testid = 1;
            var fakeProduct = new ProductViewDto
            {
                Id = testid,
                Name = "Mì tôm",
                Description = "Không có gì để nói",
                Price = 10.4m,
                SalePrice = 0,
                ImageUrl = string.Empty,
                AvgRating = 5
            };

            // mock mediaR gửi GetProducCommand với id cụ thể 
            _mediator
                .Setup(m => m.Send(It.Is<GetProductCommand>(q => q.Id == testid), default))
                .ReturnsAsync(fakeProduct);
            TestContext.WriteLine($"Bắt đầu goijv ào controller");

            // act 
            var result = await _controller.GetById(testid) as OkObjectResult;

            // assert
            Assert.IsNotNull(result);
            var returnedProduct = result.Value as ProductViewDto;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(fakeProduct, returnedProduct);
            Assert.AreEqual(fakeProduct.Id, returnedProduct.Id);
        }

        [TestMethod]
        public async Task GetById_ShouldThrowException_WhenProductNotFound()
        {
            var testId = 99;

            _mediator
                .Setup(m => m.Send(It.Is<GetProductCommand>(p => p.Id == testId), default))
                .ThrowsAsync(new NotFoundException("Không tìm thấy sản phẩm"));

             var exception = await Assert.ThrowsExactlyAsync<NotFoundException>( async () => await _controller.GetById(testId));

            Assert.AreEqual("Không tìm thấy sản phẩm", exception.Message);

            // đảm bảo mediator được gọi 
            _mediator.Verify(m => m.Send(It.IsAny<GetProductCommand>(), default), Times.Once());
        }
        [TestMethod]
        public async Task GetAllProduct_ShouldReturnOK_WithAllProducts()
        {

            // arrage
            var fakeProducts = GetProductTest().ToList();


            _mediator
                .Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                    .ReturnsAsync(fakeProducts);


            // act 
            var result = await _controller.GetAll() as OkObjectResult;

            // assert

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // check data match 
            var returnProducts = result.Value as IEnumerable<ProductViewDto>;
            Assert.IsNotNull(returnProducts);
            Assert.AreEqual(fakeProducts.Count, returnProducts.Count());

            // đảm bảo Mediator chỉ chạy một lần
            _mediator.Verify(m => m.Send(It.IsAny<GetProductsQuery>(), default), Times.Once());
        }

        [TestMethod]
        public async Task GetAllProduct_ShouldReturnMessage_WhenNoProductsFound()
        {
            // arrange
            var data = new List<ProductViewDto>();
            _mediator
                .Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                .ReturnsAsync(data);


            // act 
            var result = await _controller.GetAll() as OkObjectResult;

            var response = result.Value as ApiResponse<IEnumerable<ProductViewDto>>;
            Assert.AreEqual("Chưa có sản phẩm nào", response.Message);
            Assert.AreEqual(0, response.Data.Count());
        }
        [TestMethod]
        public async Task CreateProduct_ShouldReturnCreated_WhenSuccessfully()
        {
            // arrange
            var command = new CreateProductCommand
            {
                Name = "Samsing",
                Description = "Như cc",
                Price = 5000,
                CategoryId = 1,
                Stock = 10
            };

            _mediator
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default))
                .ReturnsAsync(Unit.Value);

            // act 
            var result = await _controller.Create(command) as ObjectResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            // check message 
            var response = result.Value as ActionResponse;
            Assert.AreEqual("Create Product successfully.", response?.Message);

            _mediator.Verify(m => m.Send(command, default), Times.Once());
        }

        [TestMethod]
        public async Task CreateProduct_ShouldThrowBadRequestException_WhenDataIsInvalid()
        {
            var command = new CreateProductCommand
            {
                Name = "",
                Description = "Chưa có sản phẩm",
                Price = 200,
                CategoryId = 2,
                Stock = 10
            };

            _mediator
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default))
                .ThrowsAsync(new BadRequestException("Tên sản phẩm không được để trống"));

            // kiểm tra xem controller có ném đúng ra exception đó hay không 
            var exception = await Assert.ThrowsExactlyAsync<BadRequestException>(async () => {
                await _controller.Create(command);
            });


            // assert 
            Assert.AreEqual("Tên sản phẩm không được để trống", exception.Message);
            TestContext.WriteLine($"Test Case vừa xong: {TestContext.TestName}");
            TestContext.WriteLine($"Kết quả cuẩ bài Test: {TestContext.CurrentTestOutcome}");
            // đảm bảo mediar đã được gọi nhưng thất bại 
            _mediator.Verify(m => m.Send(It.IsAny<CreateProductCommand>(), default), Times.Once());
            
        }
    }
}