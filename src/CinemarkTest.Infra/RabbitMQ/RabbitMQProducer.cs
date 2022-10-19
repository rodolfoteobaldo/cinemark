using System.Text;
using System.Text.Json;
using CinemarkTest.Application.Repositories;
using CinemarkTest.Domain.IntegrationEvents;
using RabbitMQ.Client;

namespace CinemarkTest.Infra.RabbitMQ;

public class RabbitMQProducer<T> : IRabbitMQProducer<T> where T : Message
{
    public void Publish (T message)  {
        var uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672/");
        var factory = new ConnectionFactory {
            Uri = uri
        };
        
        var connection = factory.CreateConnection();
        
        using
            var channel = connection.CreateModel();
        
        channel.QueueDeclare(message.Subject, true, false, false);
        
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish(exchange: "", routingKey: message.Subject, body: body);
    }
}