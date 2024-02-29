using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // ex: api/projects?query=net core
        [HttpGet]
        [Authorize(Roles = "client, freelancer")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> GetAsync([FromQuery] string? query)
        {
            var getAllProjectsQuery = new GetAllProjectsQuery(query);
            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        // ex: api/projects/3
        [HttpGet("{id:int}")]
        [Authorize(Roles = "client, freelancer")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var query = new GetProjectByIdQuery(id);
            var project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> PostAsync([FromBody] CreateProjectCommand command)
        {
            _ = await _mediator.Send(command);

            return Ok(command);
        }

        // ex: api/projects/2
        [HttpPut("{id:int}")]
        [Authorize(Roles = "client")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> PutAsync([FromRoute] int id,
                                 [FromBody] UpdateProjectCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        // ex: api/projects/3
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "client")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        // ex: api/projects/2/comments
        [HttpPost("{id:int}/comments")]
        [Authorize(Roles = "client, freelancer")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> PostCommentAsync([FromRoute] int id,
                                         [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        // ex: api/projects/1/start
        [HttpPut("{id:int}/start")]
        [Authorize(Roles = "client")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> StartAsync([FromRoute] int id)
        {
            var command = new StartProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        // ex: api/projects/1/finish
        [HttpPut("{id:int}/finish")]
        [Authorize(Roles = "client")] // Adicionando somente acesso de usuário autenticado neste método com role indicada
        public async Task<IActionResult> FinishAsync([FromRoute] int id, [FromBody] FinishProjectCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest("O pagamento não pode ser processado.");

            return Accepted();
        }
    }
}
