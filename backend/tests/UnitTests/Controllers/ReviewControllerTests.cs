using api.Controllers;
using api.Helpers.Dtos;
using application.cases.Commands.Reviews;
using application.cases.Queries.Reviews;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;

namespace UnitTests.Controllers {
    [TestClass]
    public class ReviewControllerTests
    {
        private Mock<IMediator> _mock;
        private ReviewController _controller;
        public static IEnumerable<Review> GetAllDataReview()
        {
            yield return new Review(Guid.NewGuid(), 1, 4, "Đồ ok đó");
            yield return new Review(Guid.NewGuid(), 1, 3, "Đồ binhg thường");
            yield return new Review(Guid.NewGuid(), 1, 2, "Đồ Tệ");
            yield return new Review(Guid.NewGuid(), 1, 5, "Đồ tuyệt vời lắm");
        }
        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IMediator>();
            _controller = new ReviewController(_mock.Object);
        }

        [TestMethod]
        public async Task CreateReview_ShouldReturnCreated_WithStatus201()
        {
            // arrange: chuẩn bị đầu vào 
            var command = new CreateReviewCommand
            {
                ProductEntityId = 1,
                Ratings = 4,
                Comments = "Sản phẩm này tốt quá tối đã mua nó rồi, dùng thíc lắm =))"
            };

            _mock.Setup(m => m.Send(It.IsAny<CreateReviewCommand>(), default))
                .ReturnsAsync(Unit.Value);

            // act: gọi controlelr và truyền tham số vào hàm cần test
            var result = await _controller.CreateAsync(command) as ObjectResult;
            
            // assert: Kiểm tra so sánh kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            var response = result.Value as ActionResponse;

            Assert.IsNotNull(response);
            Assert.AreEqual("Created successfully.", response.Message);

            _mock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once());
        }
        [TestMethod]
        public async Task GetReviewByUser_ShouldReturnOk_WithReviewOfUser()
        {
        }

        [TestMethod]
        public async Task GetAllReViewByProducts_ShouldReturnOk_WithDataAllReview()
        {
            // arrange 

            var data = GetAllDataReview().ToList();
            var productId = 1;
            _mock.Setup(m => m.Send(It.IsAny<GetReviewByProductQuery>(), default))
                .ReturnsAsync(data);

            var result = await _controller.GetAllReviewByProduct(productId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as IEnumerable<Review>;

            Assert.IsNotNull(response);
            Assert.AreEqual(data.Count, response.Count());
        }
    }
}