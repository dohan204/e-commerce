using application.interfaces;
using AutoMapper;
using domain.entities;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories
{
    public class CartRepository : ICartRepository
    {
        // private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _ctx;
        public CartRepository(ApplicationDbContext ctx, IMapper mapper, ICurrentUser currentUser)
        {
            _ctx = ctx;
            _mapper = mapper;
            // _currentUser = currentUser;
        }

        public async Task CreateAsync(Cart cart)
        {
            var cartInsert = _mapper.Map<CartEntity>(cart);
            await _ctx.Carts.AddAsync(cartInsert);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Cart?> GetCartByUserAsync(Guid userid)
        {
            var userCart = await _ctx.Carts.Include(e => e.Items).FirstOrDefaultAsync(e => e.UserId == userid);
            var cartMapping = _mapper.Map<Cart>(userCart);
            Console.WriteLine(cartMapping.Items.Count);
            return cartMapping;
        }
        public async Task UpdateAsync(Cart cart)
        {
            var cartEntity = await _ctx.Carts.Include(e => e.Items).FirstOrDefaultAsync(e => e.Id == cart.Id);
            _mapper.Map(cart, cartEntity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid userId, int productId)
        {
            // lay ra san pham cos co cung userId va cung productId 
            var cartItems = await _ctx.Set<CartItemEntity>()
                .FirstOrDefaultAsync(e => e.Cart.UserId == userId && e.ProductId == productId);
            
            if(cartItems is null)
                return false;
            // thucj hien xoa mem
            cartItems.IsDeleted = true;
            cartItems.DeleteAt = DateTime.UtcNow;

            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}