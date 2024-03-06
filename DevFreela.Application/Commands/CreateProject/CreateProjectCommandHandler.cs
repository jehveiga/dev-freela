using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(title: request.Title,
                          description: request.Description,
                          idClient: request.IdClient,
                          idFreelancer: request.IdFreelancer,
                          totalCost: request.TotalCost);

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.Skills.AddSkillFromProjectAsync(project);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return project.Id;
        }
    }
}
