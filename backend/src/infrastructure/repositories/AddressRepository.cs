using application.interfaces;
using infrastructure.dependency;
using domain.entities;
using AutoMapper;
using infrastructure.persistence.entities;
using Microsoft.EntityFrameworkCore;
namespace infrastructure.repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext ctx;
        public AddressRepository(ApplicationDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this._mapper = mapper;
        }

        public async Task CreateAsync(Address address)
        {
            var addressInsert = _mapper.Map<AddressEntity>(address);
            await ctx.Addresses.AddAsync(addressInsert);
            await ctx.SaveChangesAsync();
        }

        public async Task<Address> GetByUserId(Guid Id)
        {
            var address = await ctx.Addresses.Include(e => e.AppUser).FirstOrDefaultAsync(e => e.UserId == Id);
            var userAddress = _mapper.Map<Address>(address);
            return userAddress;
        }
    }
}