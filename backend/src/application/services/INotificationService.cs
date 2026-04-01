namespace application.services
{
    public interface INotificationService
    {
        Task SendNotificationToAllAsync(string message);
        Task SendNotificationToUserAsync(string userId,string title, string message);
    }
}