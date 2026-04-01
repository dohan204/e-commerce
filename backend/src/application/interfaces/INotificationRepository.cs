using domain.entities;

namespace application.interfaces
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task MarkAsReadAsync(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId);
    }
}