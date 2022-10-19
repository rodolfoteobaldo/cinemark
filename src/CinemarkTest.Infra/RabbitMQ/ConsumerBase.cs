using System.Text;
using System.Text.Json;
using CinemarkTest.Domain.IntegrationEvents;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CinemarkTest.Infra.RabbitMQ;

public abstract class ConsumerBase<T> where T : Message
{
    protected ConsumerBase(string subject)
    {
        var uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672/");
        var factory = new ConnectionFactory {
           Uri = uri
        };
        
        var connection = factory.CreateConnection();
        
        var channel = connection.CreateModel();
        
        channel.QueueDeclare(subject, true, false, false);

        StartConsuming(channel, subject);
    }

    private void StartConsuming(IModel channel, string subject)
    {
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += HandleDelivered;
        channel.BasicConsume(subject, true, consumer);
    }
    
    private async void HandleDelivered(object sender, BasicDeliverEventArgs eventArgs)
    {
        var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
        var evt = JsonSerializer.Deserialize<T>(message);
        await Handle(evt);
    }

    protected abstract Task Handle(T @event);
}