using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace BackEnd.RabbitMQ
{
    public class RabbitMQSender
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName = "direct_exchange";

        public RabbitMQSender(IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(configuration["RabbitMQ:ConnectionString"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
        }

        public void SendMessage<T>(T message, string routingKey)
        {
            var messageBody = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageBody);

            _channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: body
            );
        }
    }
}

