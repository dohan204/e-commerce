using domain.entities;

namespace application.interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserAsync(Guid UserId);
        Task CreateAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task<bool> DeleteAsync(Guid id,int Id);
    }
}