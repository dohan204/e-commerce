using application.exceptions;
using application.interfaces;
using domain.entities;
using MediatR;
using Serilog;

namespace application.cases.Queries.Addresses
{
    public class GetAddressUserHandler : IRequestHandler<GetAddressUserQuery, Address>
    {
        private readonly IAddressRepository _addressRepository;
        public GetAddressUserHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> Handle(GetAddressUserQuery query, CancellationToken  token)
        {
            var address = await _addressRepository.GetByUserId(query.UserId);
            if(address is null)
            {
                Log.Warning("Người dùng này chưa có địa chỉ..");
                throw new NotFoundException("Khong có địa chỉ tại thời điểm này.");
            }

            return address;
        }
    }
}