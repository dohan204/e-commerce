using application.interfaces;
using AutoMapper;
using domain.entities;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public VoucherRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Voucher> GetByIdAsync(int id)
        {
            var voucherEntity = await _context.Vouchers.FindAsync(id);
            var voucher = _mapper.Map<Voucher>(voucherEntity);
            return voucher;
        }
        public async Task<IReadOnlyCollection<Voucher>> GetVouchersAsync()
        {
            var voucherEntity = await _context.Vouchers.AsNoTracking().ToListAsync();
            var vouchers = _mapper.Map<IReadOnlyCollection<Voucher>>(voucherEntity);
            return vouchers;
        }
        public async Task CreateAsync(Voucher vouchers)
        {
            var voucherCreae = _mapper.Map<VoucherEntity>(vouchers);
            await _context.Vouchers.AddAsync(voucherCreae);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountVoucher()
        {
            return await _context.Vouchers
                .Where(e => e.ExpiryDate > DateTime.UtcNow && e.MaxUsage > 0)
                .CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDelete = await _context.Vouchers.FindAsync(id);
            if(isDelete is null)
            return false;

            isDelete.IsDeleted = true;
            isDelete.DeleteAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Voucher voucher)
        {
            var vou = await _context.Vouchers.FindAsync(voucher.Id);
            _mapper.Map(voucher, vou);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}