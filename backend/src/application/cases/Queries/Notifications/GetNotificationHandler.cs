using MediatR;
using domain.entities;
using application.interfaces;
namespace application.cases.Queries.Notifications
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationUser, IEnumerable<Notification>>
    {
        private readonly INotificationRepository _notification;
        public GetNotificationHandler(INotificationRepository notification)
        {
            this._notification = notification;
        }

        public async Task<IEnumerable<Notification>> Handle(GetNotificationUser query, CancellationToken token)
        {
            var notifiation = await _notification.GetNotificationsByUserIdAsync(query.UserId);
            return notifiation;
        }
    }
}