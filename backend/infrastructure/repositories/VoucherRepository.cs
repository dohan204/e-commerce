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
    }
}