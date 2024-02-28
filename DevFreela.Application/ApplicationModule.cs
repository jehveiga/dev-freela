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
                // Adicionando o servi�o do MediatR no container de servi�o do Asp.Net, adicionar� todas dependecias de servi�o pelo Assembly informado no parametro
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