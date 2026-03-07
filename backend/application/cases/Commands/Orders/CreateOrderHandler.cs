using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Commands.Orders
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly ICurrentUser currentUser;
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _repoProduct;
        public CreateOrderHandler(IOrderRepository orderRepository, ICurrentUser user, IProductRepository repoProduct)
        {
            _repository = orderRepository;
            currentUser = user;
            _repoProduct = repoProduct;
        }

        public async Task<Unit> Handle(CreateOrderCommand command, CancellationToken token)
        {
            
            if(Guid.TryParse(currentUser.UserId, out var user))
            {
                Log.Information($"Infor user: {user}");
            }

            Log.Information("Start create Order with User and product", new {user = user, product = command.OrderCode});
            var order =  Order.Create(
                user,
                command.TotalAmount,
                command.DiscountAmount,
                command.ShippingFee,
                command.ShippingAddress,
                command.VoucherId > 0 ? command.VoucherId : null,
                command.Note
            );

            foreach(var item in command.OrderItems)
            {
                var product = await _repoProduct.GetProductById(item.ProductId);
                if(product is null)
                {
                    Log.Warning($"Product with id: {item.ProductId} Not found");
                    throw new NotFoundException("Product not found");
                }
                var create = OrderItem.Create
                (
                    order.Id,
                    product.Id,
                    item.Quantity,
                    product.Price
                );
                order.Items.Add(create);
            }
            // tính tổng số tiền trong đơn hàng 

            var totalAmount = order.Items.Sum(x => x.Quantity * x.Price);
            // trả ra số tiền cuối cùng
            order.ReturnFinal(totalAmount - command.DiscountAmount + command.ShippingFee);

            Log.Information("Start insert Db,");
            await _repository.CreateAsync(order);
            return Unit.Value;
        }
    }
}