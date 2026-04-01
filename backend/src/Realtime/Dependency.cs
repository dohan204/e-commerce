using application.services;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Services;

namespace Realtime
{
    public static class RealtimeExtensions
    {
        public static IServiceCollection AddRealtime(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<INotificationService, NotificationService>();
            return services;
        }
    }
}