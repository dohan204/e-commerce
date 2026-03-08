using domain.entities;

namespace application.interfaces
{
    public interface IReviewRepository
    {
        Task CreateAsync(Review reviews);
        Task<Review> GetReviewByIdUser(Guid userId);
        Task<IReadOnlyList<Review>> GetReviewsAllByProduct(int productId);
        Task<bool> DeleteAsync(int reviewId);
    }
}