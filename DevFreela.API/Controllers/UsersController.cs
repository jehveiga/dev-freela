using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);

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
            if (!ModelState.IsValid)
            {
                var messages = ModelState.SelectMany(ms => ms.Value.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();

                return BadRequest(messages);
            }

            var idUser = await _mediator.Send(command);


            return CreatedAtAction(nameof(GetByIdAsync), new { id = idUser }, command);
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
