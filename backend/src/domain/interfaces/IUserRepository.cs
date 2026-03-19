using domain.entities;

namespace domain.interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<Guid> CreatedAsync(User user);
        Task<bool> EmailExists(string email);
        Task<IReadOnlyList<User>> GetAllUserAsync();
        Task<bool> RemoveUser(Guid id);
        Task<IEnumerable<Order>> GetAllOrderUser(Guid id);
        Task<int> GetCountUser();
    }
}