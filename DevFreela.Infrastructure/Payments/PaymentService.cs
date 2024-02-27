﻿using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentBaseUrl;

        public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentBaseUrl = configuration.GetSection("Services:Payments").Value ?? string.Empty;
        }

        public async Task<bool> ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            var url = $"{_paymentBaseUrl}/api/payments";
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);
            var paymentInfoContent = new StringContent(paymentInfoJson, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Payments");

            var response = await httpClient.PostAsync(url, paymentInfoContent);

            return response.IsSuccessStatusCode;
        }
    }
}
