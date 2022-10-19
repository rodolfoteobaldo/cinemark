using CinemarkTest.Infra.MongoDB.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CinemarkTest.Infra.MongoDB;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoDbSettings> dbSettings)
    {
        var mongoDbSettings = dbSettings.Value;

        var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(mongoDbSettings.ConnectionString));
        var mongoClient = new MongoClient(mongoClientSettings);
        _database = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
    }
    
    public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name);
}