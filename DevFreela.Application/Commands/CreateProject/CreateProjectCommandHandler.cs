using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(title: request.Title,
                          description: request.Description,
                          idClient: request.IdClient,
                          idFreelancer: request.IdFreelancer,
                          totalCost: request.TotalCost);

            await _projectRepository.AddAsync(project);

            return project.Id;
        }
    }
}
