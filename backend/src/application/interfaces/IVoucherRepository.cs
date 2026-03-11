using domain.entities;

namespace application.interfaces
{
    public interface IVoucherRepository
    {
        Task CreateAsync(Voucher vourchers);
        Task<Voucher> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Voucher>> GetVouchersAsync();
    }
}