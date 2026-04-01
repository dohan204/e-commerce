using application.interfaces;
using infrastructure.identity;

namespace infrastructure.persistence.entities
{
    public class NotificationEntity : ISoftDelete
    {
        public int Id {get; set;}
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; } 
        public DateTimeOffset? DeleteAt { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}