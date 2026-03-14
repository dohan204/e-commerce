using domain.exceptions;

namespace domain.entities
{
    public enum RatingsReview {so_good, good, normal, bad, too_bad }
    public class Review: BaseEntity
    {
        public Guid UserId { get; private set; }
        public int ProductId { get; private set; }
        public decimal Rating {get; private set;}
        public string Comment {get; private set;} = string.Empty;
        public DateTime Created_At {get; private set;}

        private Review() {}
        public Review(
            Guid userId,
            int productId,
            decimal rating,
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

        public static Review Create(Guid userId, int productId, decimal rating, string comment)
        {
            if(string.IsNullOrEmpty(comment))
            {
                throw new DomainException("NỘi dung Đánh giá không được để trống");
            } 
            return new Review(userId, productId, rating, comment);
        }
    }
}