using Ambev.DeveloperEvaluation.Domain.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Serilog;
using System.Text;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Bus
{
    public class Producer : IRabbitMqProducer
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger _logger;

        public Producer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMqHost"],
                Port = int.Parse(_configuration["RabbitMqPort"]),
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _logger = Log.ForContext<Producer>();
        }

        public void Publish(string evento, Sale sale)
        {
            var mensagem = JsonSerializer.Serialize(new { Evento = evento, Dados = sale });
            var body = Encoding.UTF8.GetBytes(mensagem);

            _logger.Information(mensagem);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
        }
    }
}
