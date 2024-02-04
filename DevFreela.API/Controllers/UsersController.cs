using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
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
        public IActionResult Post([FromBody] CreateUserInputModel inputModel)
        {
            var idUser = _userService.Create(inputModel);


            return CreatedAtAction(nameof(GetById), new { id = idUser }, inputModel);
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
