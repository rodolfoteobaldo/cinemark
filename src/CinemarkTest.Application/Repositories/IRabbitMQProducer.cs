namespace CinemarkTest.Application.Repositories;

public interface IRabbitMQProducer<in T>
{
    void Publish(T message);
}