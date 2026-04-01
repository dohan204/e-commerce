using Microsoft.AspNetCore.SignalR;

namespace api.Hubs
{
    public interface INotification
    {
        Task ReceiveMessage(string message);
    }
    public class NotificationHub : Hub
    {
        
    }
}