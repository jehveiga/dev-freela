using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        // api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            // Buscar todos ou filtrar

            return Ok();
        }

        // api/projects/3
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Buscar o projeto

            // return NotFound();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            if (createProject.Title.Length > 50)
            {
                return BadRequest();
            }

            // Cadastrar o projeto

            return CreatedAtAction(nameof(GetById), new { id = createProject.Id }, createProject);
        }

        // api/projects/2
        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id,
                                 [FromBody] UpdateProjectModel updateProject)
        {
            if (updateProject.Description.Length > 200)
            {
                return BadRequest();
            }

            // Atualizo o projeto
            return NoContent();
        }

        // api/projects/3
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // Buscar, se não existir retorna NotFound

            // Remover

            return NoContent();
        }

        // api/projects/2/comments
        [HttpPost("{id:int}/comments")]
        public IActionResult PostComment([FromRoute] int id,
                                         [FromBody] CreateCommentModel createComment)
        {
            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id:int}/start")]
        public IActionResult Start([FromRoute] int id)
        {
            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id:int}/finish")]
        public IActionResult Finish([FromRoute] int id)
        {
            return NoContent();
        }
    }
}
