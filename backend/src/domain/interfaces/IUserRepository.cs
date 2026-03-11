using domain.entities;

namespace domain.interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<Guid> CreatedAsync(User user);
        Task<bool> EmailExists(string email);
        Task<IReadOnlyList<User>> GetAllUserAsync();
        Task RemoveUser(Guid id);
    }
}