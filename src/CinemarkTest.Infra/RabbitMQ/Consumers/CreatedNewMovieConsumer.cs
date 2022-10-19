using CinemarkTest.Domain.IntegrationEvents;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace CinemarkTest.Infra.RabbitMQ.Consumers;

public class CreatedNewMovieConsumer : ConsumerBase<CreatedMovieEvent>, IHostedService
{
    private readonly IRedisDatabase _redisDatabase;

    public CreatedNewMovieConsumer(IRedisDatabase redisDatabase) 
        : base(CreatedMovieEvent.Subject)
    {
        _redisDatabase = redisDatabase;
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    
    protected override Task Handle(CreatedMovieEvent @event)
    {
        _redisDatabase.RemoveAsync("all_movies");
        
        return Task.CompletedTask;
    }
}