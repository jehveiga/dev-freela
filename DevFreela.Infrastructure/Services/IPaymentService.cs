using DevFreela.Infrastructure.Payments;

namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        void ProcessPayment(PaymentInfoDto paymentInfoDto);
    }
}