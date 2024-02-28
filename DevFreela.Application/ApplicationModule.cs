using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                // Adicionando o serviço do MediatR no container de serviço do Asp.Net, adicionará todas dependecias de serviço pelo Assembly informado no parametro
                .AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(CreateProjectCommand).Assembly); })
                .AddConsumers();

            return services;
        }

        private static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddHostedService<PaymentApprovedConsumer>();

            return services;
        }
    }
}