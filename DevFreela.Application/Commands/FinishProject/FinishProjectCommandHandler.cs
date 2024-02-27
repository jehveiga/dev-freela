using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            // Obtem o projeto referente ao id
            var project = await _projectRepository.GetByIdAsync(request.Id);

            // Monta o objeto DTO
            var paymentInfoDto = new PaymentInfoDto(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, request.Amount);

            // Envia os dados para processamento de pagamento
            _paymentService.ProcessPayment(paymentInfoDto);

            // Define o projeto para pagamento pendente
            project.SetPaymentPerding();

            await _projectRepository.SaveChangesAsync();

            return true;
        }
    }
}
