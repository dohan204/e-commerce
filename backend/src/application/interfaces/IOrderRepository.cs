using domain.entities;

namespace application.interfaces
{
    public class RevenueDto
    {
        public DateTime Date {get; set;}
        public decimal Value {get; set;}
    }


    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetAll();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RevenueDto>> GetDecimalAsync();
    }
}