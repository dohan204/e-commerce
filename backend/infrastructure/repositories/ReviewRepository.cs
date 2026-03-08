using System.Security.Cryptography;
using application.interfaces;
using AutoMapper;
using domain.entities;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser current;
        public ReviewRepository(ApplicationDbContext context, 
                ICurrentUser current, IMapper mapper)
        {
            _context = context;
            this.current = current;
            _mapper = mapper;
        }

        public async Task CreateAsync(Review reviews)
        {
            var reviewInsert = _mapper.Map<ReviewEntity>(reviews);
            await _context.Reviews.AddAsync(reviewInsert);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetReviewByIdUser(Guid id)
        {
            var userReview = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(e => e.UserId == id);
            var review = _mapper.Map<Review>(userReview);
            return review;
        }

        public async Task<IReadOnlyList<Review>> GetReviewsAllByProduct(int productId)
        {
            var reviewResponse = await _context.Reviews.AsNoTracking().Where(e => e.ProductEntityId == productId)
                .ToListAsync();

            var reviews = _mapper.Map<IReadOnlyList<Review>>(reviewResponse);
            return reviews;
        }
        public async Task<bool> DeleteAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            review.IsDeleted = true;
            review.DeleteAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}