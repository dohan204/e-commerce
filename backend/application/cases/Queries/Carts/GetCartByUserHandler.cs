using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;

namespace application.cases.Queries.Carts
{
    public class GetCartByUserHandler : IRequestHandler<GetCartByUserQuery, Cart>
    {
        private readonly ICartRepository cartRepository;
        public GetCartByUserHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public async Task<Cart> Handle(GetCartByUserQuery query, CancellationToken token)
        {
            var cart = await cartRepository.GetCartByUserAsync(query.Userid);
            if(cart is null)
                throw new NotFoundException("cart Not found");
            
            return cart;
        }
    }
}