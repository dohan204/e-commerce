using application.exceptions;
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
            var isDelete = await _reviewRepository.DeleteAsync(command.ReviewId);
            if(!isDelete)
            {
                throw new NotFoundException("Không tìm thấy bài đánh giá");
            }
            return true;
        }

    }
}