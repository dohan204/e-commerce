using domain.exceptions;

namespace domain.entities
{
    public enum RatingsReview {so_good, good, normal, bad, too_bad }
    public class Reviews: BaseEntity
    {
        public Guid UserId { get; private set; }
        public int ProductId { get; private set; }
        public RatingsReview Rating {get; private set;} = RatingsReview.normal;
        public string Comment {get; private set;} = string.Empty;
        public DateTime Created_At {get; private set;}

        protected Reviews() {}
        private Reviews(
            Guid userId,
            int productId,
            RatingsReview rating,
            string comment
        )
        {
            if(userId == Guid.Empty) 
                throw new DomainException("User invalid");
            if(productId < 0)
            {
                throw new DomainException("Product invalid");
            }
            UserId = userId;
            ProductId = productId;
            Rating = rating;
            Comment = comment;
            Created_At = DateTime.UtcNow;
        }

        public static Reviews Create(Guid userId, int productId, RatingsReview rating, string comment)
        {
            if(string.IsNullOrEmpty(comment))
            {
                throw new DomainException("NỘi dung Đánh giá không được để trống");
            } 
            return new Reviews(userId, productId, rating, comment);
        }
    }
}