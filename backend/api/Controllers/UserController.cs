using application.cases.Commands.Users;
using application.cases.Queries.Users;
using domain.entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserHandler _createUserHandler;
        private readonly HandlerGetUserById _handler;
        private readonly GetAllUserQuery _getAll;
        public UserController(HandlerGetUserById handler, CreateUserHandler createUserHandler, GetAllUserQuery getAllUserQuery)
        {
            _handler = handler;
            _createUserHandler = createUserHandler;
            _getAll = getAllUserQuery;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _handler.Handle(id);
            if(result is null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateUserCommand command) 
        {
            await _createUserHandler.Handle(command);
            return StatusCode(StatusCodes.Status201Created,new {
                Status = StatusCodes.Status201Created,
                Message = "Created Successfully"
            });
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _getAll.Handle();
            if(!result.Any())
                return Ok(new
                {
                    message = "chưa có người dùng nào",
                    data = result
                });
            return Ok(result);
        }
    }
}