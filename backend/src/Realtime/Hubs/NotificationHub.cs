using Microsoft.AspNetCore.SignalR;
using Realtime.Dtos;
using Serilog;

namespace Realtime.Hubs
{
    public interface INotificationHub
    {
        Task ReceiveMessage(string message);
        Task ReceiveNotification(NotificationDto notification);
    }
    public class NotificationHub : Hub<INotificationHub>
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            Log.Information($@"NotificationHub: {userId}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            Log.Information($@"NotificationHub: {userId}");
            await base.OnDisconnectedAsync(exception);
        }
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();  
        }
    }
}