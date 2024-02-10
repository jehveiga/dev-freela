using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // api/users
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserCommand command)
        {
            var idUser = await _mediator.Send(command);


            return CreatedAtAction(nameof(GetById), new { id = idUser }, command);
        }

        // api/users/1/login
        [HttpPut("{id:int}/login")]
        public IActionResult Login([FromRoute] int id,
                                   [FromBody] LoginModel loginModel)
        {
            return NoContent();
        }
    }
}
