using application.interfaces;
using AutoMapper;
using domain.entities;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public NotificationRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddNotificationAsync(Notification notification)
        {
            var data = _mapper.Map<NotificationEntity>(notification);
            _context.Notifications.Add(data);
            await _context.SaveChangesAsync();
        }
        public async Task MarkAsReadAsync(Notification notification)
        {
            var notificationEntity = await _context.Notifications.FindAsync(notification.Id);
            _mapper.Map(notification, notificationEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<domain.entities.Notification>> GetNotificationsByUserIdAsync(Guid userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return notifications.Select(n => new domain.entities.Notification(
                userId: n.UserId,
                title: n.Title,
                message: n.Message
            )
            {
                Id = n.Id,
                CreatedAt = n.CreatedAt,
                IsRead = n.IsRead
            });
        }
    }
}