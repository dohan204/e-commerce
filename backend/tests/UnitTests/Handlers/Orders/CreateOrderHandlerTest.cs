using System.Runtime.CompilerServices;
using api.Controllers;
using application.cases.Commands.Orders;
using application.cases.Queries.Vouchers;
using application.exceptions;
using application.interfaces;
using domain.entities;
using Moq;

namespace UnitTests.Handlers.Orders
{
    [TestClass]
    public class CreateOrderHandlerTest
    {
        private Mock<IOrderRepository> _orderRepo;
        private Mock<IProductRepository> _productRepo;
        private Mock<ICurrentUser> _currentUser;
        private Mock<IVoucherRepository> _voucherRepo;
        private Mock<IAddressRepository> _addressRepo;
        private CreateOrderHandler _handler;
        [TestInitialize]
        public void Setup()
        {
            _orderRepo = new Mock<IOrderRepository>();
            _productRepo = new Mock<IProductRepository>();
            _currentUser = new Mock<ICurrentUser>();
            _voucherRepo = new Mock<IVoucherRepository>();
            _addressRepo = new Mock<IAddressRepository>();

            _handler = new CreateOrderHandler(
                _orderRepo.Object,
                _currentUser.Object,
                _productRepo.Object,
                _voucherRepo.Object,
                _addressRepo.Object
            );
        }

        [TestMethod]
        public async Task Handle_ShouldUpdateStock_WhenOrderIsSuccess()
        {
            // arrange 
            var userId = Guid.NewGuid();
            _currentUser.Setup(c => c.UserId).Returns(userId.ToString());

            var product = new Products(1,"Iphone16", "khong co gi de noica", 300, 10, 1);
            _productRepo.Setup(e => e.GetProductById(product.Id))
                .ReturnsAsync(product);

            var addressUser = new Address(userId, "ha noi", "ha dong", "yen lo", "0382068238");
            _addressRepo
                .Setup(e => e.GetByUserId(userId))
                    .ReturnsAsync(addressUser);

            
            var command = new CreateOrderCommand
            {
                ShippingAddress = addressUser.ToString(),
                VoucherId = 0,
                Note = "test Order",
                OrderItems = new List<CreateOrderItem>
                {
                    new CreateOrderItem {ProductId = 1, OrderId = 1, Price = product.Price, Quantity = 5}
                }
            };

            // act 
            await _handler.Handle(command, CancellationToken.None);

            // assert
            _orderRepo.Verify(e => e.CreateAsync(It.IsAny<Order>()), Times.Once());
            _productRepo.Verify(e => e.UpdateAsync(It.IsAny<Products>()), Times.Once());
            Assert.AreEqual(5, product.Stock);
        }

        [TestMethod]
        public async Task Handle_ShouldThrowException_WhenProductNotFound()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUser.Setup(e => e.UserId).Returns(userId.ToString());

            var productId = 1;
            _productRepo.Setup(e => e.GetProductById(productId))
                .ReturnsAsync((Products?)null);
            
            var addressUser = new Address(userId, "ha noi", "ha dong", "yen lo", "0382068238");
            _addressRepo
                .Setup(e => e.GetByUserId(userId))
                    .ReturnsAsync(addressUser);
            var command = new CreateOrderCommand
            {
                ShippingAddress = addressUser.ToString(),
                VoucherId = 0,
                Note = "test Order",
                OrderItems = new List<CreateOrderItem>
                {
                    new CreateOrderItem {ProductId = 1, Price = 20, Quantity = 5}
                }
            };
            // act
            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
            _orderRepo.Verify(e => e.CreateAsync(It.IsAny<Order>()), Times.Never());
            // // Assert.IsNull(productId);
            // Assert.AreEqual(0, productId);

        }
        [TestMethod]
        public async Task Handle_ShouldThrowException_WhenStockNotEnough()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUser.Setup(e => e.UserId).Returns(userId.ToString());

            var product = new Products(1,"dien thoai", "dien thoaoi thong minh", 2000, 5, 1);
            _productRepo.Setup(e => e.GetProductById(product.Id))
                .ReturnsAsync(product);

            var address = new Address(userId, "ha noi", "ha dong", "yen lo", "0983745345");
            _addressRepo.Setup(e => e.GetByUserId(userId))
                .ReturnsAsync(address);

            var command = new CreateOrderCommand
            {
                ShippingAddress = address.ToString(),
                Note = "cac",
                OrderItems = new List<CreateOrderItem>
                {
                    new CreateOrderItem {ProductId = 1, Price = 2000, Quantity = 10}
                }
            };

            await Assert.ThrowsAsync<BussinesErrorException>( async () => await _handler.Handle(command, CancellationToken.None));
            
        }
        [TestMethod]
        public async Task Handle_ThrowException404_WhenVoucherNotFound()
        {
            // arrange 
            var userId = Guid.NewGuid();
            _currentUser.Setup(e => e.UserId)
                .Returns(userId.ToString());

            var product = new Products(1, "dien thoai", "Khong co gi", 20003, 10, 1);
            _productRepo.Setup(e => e.GetProductById(product.Id))
                .ReturnsAsync(product);

            var addressUser = new Address(userId, "ha noi", "ha dong", "Yen lo", "098843453");
            _addressRepo.Setup(e => e.GetByUserId(userId))
                .ReturnsAsync(addressUser);

            var voucherId = 1;
            _voucherRepo.Setup(e => e.GetByIdAsync(voucherId))
                .ReturnsAsync((Voucher?)null);

            var command = new CreateOrderCommand
            {
                ShippingAddress = addressUser.ToString(),
                VoucherId = voucherId,
                OrderItems = new List<CreateOrderItem>
                {
                    new CreateOrderItem {ProductId = 1, Price = 200, Quantity = 10}
                }
            };

            await Assert.ThrowsAsync<NotFoundException>( async () => await _handler.Handle(command, CancellationToken.None));
        }
        [TestMethod]
        public async Task Handle_ShouldApplyVoucherDiscount()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUser.Setup(e => e.UserId).Returns(userId.ToString());

            var product = new Products(1, "phone", "desc", 100, 10, 1);
            _productRepo.Setup(e => e.GetProductById(product.Id))
                .ReturnsAsync(product);

            var voucher = new Voucher(1,"Percent", 20, 50, 100, DateTime.UtcNow.AddDays(7));
            _voucherRepo.Setup(e => e.GetByIdAsync(1))
                .ReturnsAsync(voucher);

            var address = new Address(userId, "hn", "hd", "yl", "098398454");
            _addressRepo.Setup(e => e.GetByUserId(userId))
                .ReturnsAsync(address);

            var command = new CreateOrderCommand
            {
                VoucherId = 1,
                OrderItems = new List<CreateOrderItem>
                {
                    new CreateOrderItem {ProductId = 1, Price = 100, Quantity = 5}
                }
            };

            await _handler.Handle(command, CancellationToken.None);
            _orderRepo.Verify(e => e.CreateAsync(It.IsAny<Order>()), Times.Once());
        }
    }
}