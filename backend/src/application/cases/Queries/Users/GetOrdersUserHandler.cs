using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.cases.Queries.Users
{
    public class GetOrdersUserHandler : IRequestHandler<GetOrdersUserQuery, IEnumerable<Order>>
    {
        private readonly IUserRepository _userRepository;
        public GetOrdersUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersUserQuery query, CancellationToken token)
        {
            var userOrder = await _userRepository.GetAllOrderUser(query.UserId);
            return userOrder;
        }
    }
}