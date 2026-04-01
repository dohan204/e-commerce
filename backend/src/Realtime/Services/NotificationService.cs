using application.services;
using Microsoft.AspNetCore.SignalR;
using Realtime.Dtos;
using Realtime.Hubs;

namespace Realtime.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
        public NotificationService(IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationToAllAsync(string message)
        {
            await _hubContext.Clients.All.ReceiveMessage(message);
        }

        public async Task SendNotificationToUserAsync(string userId, string title, string message)
        {
            await _hubContext.Clients.User(userId).ReceiveNotification(new NotificationDto
            {
                Title = title,
                Message = message
            });
        }
    }
}