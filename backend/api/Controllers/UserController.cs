using application.cases.Commands.Users;
using application.cases.Queries.Users;
using domain.entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _handler.Handle(id);
            if(result is null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateUserCommand), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(CreateUserCommand command) 
        {
            await _createUserHandler.Handle(command);
            return StatusCode(StatusCodes.Status201Created,new {
                Status = StatusCodes.Status201Created,
                Message = "Created Successfully"
            });
        }
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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