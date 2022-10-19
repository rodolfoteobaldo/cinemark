using CinemarkTest.Application.Repositories;
using CinemarkTest.Domain.IntegrationEvents;
using CinemarkTest.Infra.MongoDB;
using CinemarkTest.Infra.MongoDB.Abstractions;
using CinemarkTest.Infra.RabbitMQ;
using CinemarkTest.Infra.RabbitMQ.Consumers;
using CinemarkTest.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace CinemarkTest.Infra.Configuration;

public static class InfraConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMongoContext, MongoContext>();
        
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.AddSingleton<CreatedNewMovieConsumer>();
        services.AddSingleton<UpdatedMovieConsumer>();
        services.AddSingleton<DeletedMovieConsumer>();
        services.AddSingleton<IHostedService, CreatedNewMovieConsumer>();
        services.AddSingleton<IHostedService, UpdatedMovieConsumer>();
        services.AddSingleton<IHostedService, DeletedMovieConsumer>();
        return services;
    }
    
    public static IServiceCollection AddProducers(this IServiceCollection services)
    {
        services.AddScoped<IRabbitMQProducer<CreatedMovieEvent>, RabbitMQProducer<CreatedMovieEvent>>();
        services.AddScoped<IRabbitMQProducer<UpdatedMovieEvent>, RabbitMQProducer<UpdatedMovieEvent>>();
        services.AddScoped<IRabbitMQProducer<DeletedMovieEvent>, RabbitMQProducer<DeletedMovieEvent>>();
        return services;
    }

    public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfiguration = configuration.GetSection("RedisConfiguration").Get<RedisConfiguration>();
        services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
        services.AddSingleton<IRedisDatabase>(x =>
            x.GetRequiredService<IRedisCacheClient>().GetDbFromConfiguration());
    }
}