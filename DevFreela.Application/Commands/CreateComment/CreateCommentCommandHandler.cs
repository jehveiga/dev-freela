using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(content: request.Content,
                                 idProject: request.IdProject,
                                 idUser: request.IdUser);

            await _unitOfWork.Projects.AddCommentAsync(comment);

            return Unit.Value;
        }
    }
}
