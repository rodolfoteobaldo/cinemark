using CinemarkTest.Domain.IntegrationEvents;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace CinemarkTest.Infra.RabbitMQ.Consumers;

public class UpdatedMovieConsumer : ConsumerBase<UpdatedMovieEvent>, IHostedService
{
    private readonly IRedisDatabase _redisDatabase;

    public UpdatedMovieConsumer(IRedisDatabase redisDatabase) 
        : base(UpdatedMovieEvent.Subject)
    {
        _redisDatabase = redisDatabase;
    }
    
    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    
    protected override Task Handle(UpdatedMovieEvent @event)
    {
        _redisDatabase.RemoveAsync("all_movies");
        
        return Task.CompletedTask;
    }
}