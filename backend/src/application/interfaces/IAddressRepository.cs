using domain.entities;

namespace application.interfaces
{
    public interface IAddressRepository
    {
        Task CreateAsync(Address address);
        Task<Address> GetByUserId(Guid userId);
        // Task UpdateAsync(Address address);
    }
}