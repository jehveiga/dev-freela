using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConfiguration _configuration;

        public MessageBusService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }
        public void Publish(string queue, byte[] message)
        {
            // Criando a conexão com RabbitMQ
            using (var connection = _factory.CreateConnection())
            {
                // Criando o canal responsável para gerenciamento de filas
                using (var channel = connection.CreateModel())
                {
                    // Garantir que a fila esteja criada
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    // Publicar a mensagem
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: message);
                }
            }
        }
    }
}
