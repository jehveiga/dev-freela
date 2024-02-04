using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        // ex: api/projects?query=net core
        [HttpGet]
        public IActionResult Get([FromQuery] string query)
        {
            var projects = _projectService.GetAll(query);

            return Ok(projects);
        }

        // ex: api/projects/3
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectInputModel inputModel)
        {
            if (inputModel.Title.Length > 50)
            {
                return BadRequest();
            }

            var idProject = _projectService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = idProject }, inputModel);
        }

        // ex: api/projects/2
        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id,
                                 [FromBody] UpdateProjectInputModel inputModel)
        {
            if (inputModel.Description.Length > 200)
            {
                return BadRequest();
            }

            _projectService.Update(inputModel);

            return NoContent();
        }

        // ex: api/projects/3
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _projectService.Delete(id);

            return NoContent();
        }

        // ex: api/projects/2/comments
        [HttpPost("{id:int}/comments")]
        public IActionResult PostComment([FromRoute] int id,
                                         [FromBody] CreateCommentInputModel inputModel)
        {
            _projectService.CreateComment(inputModel);

            return NoContent();
        }

        // ex: api/projects/1/start
        [HttpPut("{id:int}/start")]
        public IActionResult Start([FromRoute] int id)
        {
            _projectService.Start(id);

            return NoContent();
        }

        // ex: api/projects/1/finish
        [HttpPut("{id:int}/finish")]
        public IActionResult Finish([FromRoute] int id)
        {
            _projectService.Finish(id);

            return NoContent();
        }
    }
}
