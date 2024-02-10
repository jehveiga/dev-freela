using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetProjectByIdQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                        .Include(p => p.Cliente)
                        .Include(p => p.Freelancer)
                        .SingleOrDefaultAsync(p => p.Id == request.Id);

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
