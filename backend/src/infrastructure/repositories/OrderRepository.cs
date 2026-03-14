using application.cases.Queries.Users;
using application.interfaces;
using AutoMapper;
using domain.entities;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories {
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _ctx;
        public OrderRepository(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task CreateAsync(Order order)
        {
            dynamic orderCreate = _mapper.Map<OrderEntity>(order);
            await _ctx.Orders.AddAsync(orderCreate);
            await _ctx.SaveChangesAsync();
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var orderEntity = await _ctx.Orders.Include(e => e.Items).FirstOrDefaultAsync(e => e.Id == id);
            var order = _mapper.Map<Order>(orderEntity);
            return order;
        }
        public async Task<IReadOnlyList<Order>> GetAll()
        {
            var orders = await _ctx.Orders.Include(e => e.Items)
            .AsNoTracking().ToListAsync();
            var ordersDomain = _mapper.Map<List<Order>>(orders);
            return ordersDomain;
        }
        public async Task UpdateAsync(Order order)
        {
                var orderEntity = await _ctx.Orders.Include(e => e.Items).FirstOrDefaultAsync(e => e.Id == order.Id);
                // _ctx.Orders.Update(updateOrder);
                _mapper.Map(order, orderEntity);
                await _ctx.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _ctx.Orders.FindAsync(id);
            // foreach(var item in order.)
            order.IsDeleted = true;
            order.DeleteAt = DateTime.UtcNow;
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task Payment()
        {
            
        }

    }
}