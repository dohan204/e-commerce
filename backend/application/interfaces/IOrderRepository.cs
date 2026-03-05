using domain.entities;

namespace application.interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetAll();
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);
    }
}