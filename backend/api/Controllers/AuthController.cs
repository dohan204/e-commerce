using application.cases.Commands.Authentication;
using application.cases.Queries.Authentication;
using api.Helpers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    // public record UserLoginRequest(string userName, string password);
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly EmailConfirmHanler _emailConfirmHanler;
        private readonly GetUserLoginHandler _userLoginHandler;
        public AuthController(EmailConfirmHanler emailConfirmHanler, GetUserLoginHandler userLoginHandler)
        {
            _emailConfirmHanler = emailConfirmHanler;
            _userLoginHandler = userLoginHandler;
        }
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequest userLogin)
        {
            var result = await _userLoginHandler.Handle(
                new GetUserLoginCommand(userLogin.userName, userLogin.password)
            );
            return Ok(result);
        }
        [HttpGet("email-confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            await _emailConfirmHanler.Handle(
                new EmailConfirmCommand(userId, token)
            );

            return Ok(new {message = "Xác nhận thành công"});
        }
    }
}