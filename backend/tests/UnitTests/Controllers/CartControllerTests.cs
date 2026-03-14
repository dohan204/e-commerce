using api.Controllers;
using application.interfaces;
using MediatR;
using Moq;

namespace UnitTests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        public TestContext TestContext {get; set;}
        private Mock<IMediator> _mock;
        private CartController _controller;
        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IMediator>();
            _controller = new CartController(_mock.Object);
        }


    }
}