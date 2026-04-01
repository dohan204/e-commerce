using MediatR;
using domain.entities;
namespace application.cases.Queries.Notifications
{
    public class GetNotificationUser : IRequest<IEnumerable<Notification>>
    {
        public Guid UserId {get; set;}
    }
}