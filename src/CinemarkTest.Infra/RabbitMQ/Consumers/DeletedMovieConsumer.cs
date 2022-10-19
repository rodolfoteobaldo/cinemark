using CinemarkTest.Domain.IntegrationEvents;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace CinemarkTest.Infra.RabbitMQ.Consumers;

public class DeletedMovieConsumer : ConsumerBase<DeletedMovieEvent>, IHostedService
{
    private readonly IRedisDatabase _redisDatabase;

    public DeletedMovieConsumer(IRedisDatabase redisDatabase) 
        : base(DeletedMovieEvent.Subject)
    {
        _redisDatabase = redisDatabase;
    }
    
    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    
    protected override Task Handle(DeletedMovieEvent @event)
    {
        _redisDatabase.RemoveAsync("all_movies");

        return Task.CompletedTask;
    }
}