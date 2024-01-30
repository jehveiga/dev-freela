using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        // api/users/1
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok();
        }

        // api/users
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel createUserModel)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, createUserModel);
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
