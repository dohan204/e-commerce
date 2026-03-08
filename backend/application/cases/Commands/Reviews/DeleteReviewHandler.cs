using application.interfaces;
using MediatR;

namespace application.cases.Commands.Reviews
{
    public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, bool>
    {
        private readonly IReviewRepository _reviewRepository;
        public DeleteReviewHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> Handle(DeleteReviewCommand command, CancellationToken tokne)
        {
            await _reviewRepository.DeleteAsync(command.ReviewId);
            return true;
        }

    }
}