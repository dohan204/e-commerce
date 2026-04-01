using application.interfaces;
using application.services;
using MediatR;

namespace application.cases.Commands.Notifications
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService notificationService;

        public CreateNotificationHandler(INotificationRepository notificationRepository, INotificationService notificationService)
        {
            _notificationRepository = notificationRepository;
            this.notificationService = notificationService;
        }

        public async Task<Unit> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new domain.entities.Notification(
                userId: Guid.Parse(request.UserId),
                title: request.Title,
                message: request.Message
            );
            await _notificationRepository.AddNotificationAsync(notification);

            await notificationService.SendNotificationToUserAsync(request.UserId, request.Title, request.Message);
            return Unit.Value;
        }
    }
}