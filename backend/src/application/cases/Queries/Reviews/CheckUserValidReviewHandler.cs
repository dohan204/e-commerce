using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Queries.Reviews
{
    public class CheckUserValidReviewHandler : IRequestHandler<CheckUserValidReview, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUser _currentUser;
        public CheckUserValidReviewHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            ICurrentUser currentUser
        )
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(CheckUserValidReview review, CancellationToken token)
        {

            // lấy Mã người dùng
            if(!Guid.TryParse(_currentUser.UserId, out var userId))
            {
                Log.Warning("Người dùng không hợp lệ");
                throw new UnauthorizeException("Người dùng không hợp lệ");
            }

            // Lấy ra đơn hàng mà nguoif dùng 
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            // check sản phẩm trong từng đơn hàng
            return orders.Where(o => o.Status == StatusOrder.delivered)
                .SelectMany(e => e.Items)
                .Any(item => item.ProductId == review.ProductId);
        }
    }
}