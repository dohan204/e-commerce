using domain.entities;
using MediatR;

namespace application.cases.Queries.Addresses
{
    public class GetAddressUserQuery: IRequest<Address>
    {
        public Guid UserId {get; set;}
    }
}