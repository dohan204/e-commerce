using System.ComponentModel;

namespace domain.entities
{
    public class Notification
    {
        public int Id { get; set; }
        public Guid UserId { get; set; } 
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false;
        private Notification() { }
        public Notification(Guid userId, string title, string message)
        {
            UserId = userId;
            Title = title;
            Message = message;
            CreatedAt = DateTime.UtcNow;
        }
        
        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}