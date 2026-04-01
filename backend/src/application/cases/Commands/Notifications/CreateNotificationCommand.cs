using MediatR;

namespace application.cases.Commands.Notifications
{
    public class CreateNotificationCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}