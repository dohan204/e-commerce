using domain.interfaces;
using MediatR;

namespace application.cases.Queries.Users
{
    public class GetCountUserHandler : IRequestHandler<GetCountUserQuery, int>
    {
        private readonly IUserRepository userRepository;
        public GetCountUserHandler(IUserRepository userRepository) => 
        this.userRepository = userRepository;

        public async Task<int> Handle(GetCountUserQuery query, CancellationToken token)
        {
            return await userRepository.GetCountUser();
        }
    }
}