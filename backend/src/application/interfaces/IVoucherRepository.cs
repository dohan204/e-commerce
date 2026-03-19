using domain.entities;

namespace application.interfaces
{
    public interface IVoucherRepository
    {
        Task CreateAsync(Voucher vourchers);
        Task<Voucher> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Voucher>> GetVouchersAsync();
        Task<int> GetCountVoucher();
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Voucher vourchers);
    }
}