﻿using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommand : IRequest<bool>
    {
        public FinishProjectCommand(string creditCardnumber, string cvv, string expiresAt, string fullName, decimal amount)
        {
            CreditCardNumber = creditCardnumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
            Amount = amount;
        }

        public int Id { get; set; }
        public string CreditCardNumber { get; private set; }
        public string Cvv { get; private set; }
        public string ExpiresAt { get; private set; }
        public string FullName { get; private set; }
        public decimal Amount { get; private set; }
    }
}
