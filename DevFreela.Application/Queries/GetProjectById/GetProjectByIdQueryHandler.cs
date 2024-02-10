using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project == null)
                return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                id: project.Id,
                title: project.Title,
                description: project.Description,
                totalCost: project.TotalCost,
                startedAt: project.StartedAt,
                finishedAt: project.FinisishedAt,
                clientFullName: project.Cliente.FullName,
                freelancerFullName: project.Freelancer.FullName);

            return projectDetailsViewModel;
        }
    }
}
