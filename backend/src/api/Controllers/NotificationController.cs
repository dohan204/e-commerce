using api.Helpers.Dtos;
using application.cases.Queries.Notifications;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotificationUser(Guid userId)
        {
            var query = new GetNotificationUser { UserId = userId };
            var notifications = await _mediator.Send(query);
            if(!notifications.Any())
            {
                return Ok(new ApiResponse<IEnumerable<Notification>>
                {
                    Message = "Không có dữ liệu thông báo",
                    Data = notifications
                });
            }

            return Ok(new ApiResponse<IEnumerable<Notification>>
            {
                Message = "Lấy dữ liệu thành công",
                Data = notifications
            });
        }
    }
}