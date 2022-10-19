using MongoDB.Driver;

namespace CinemarkTest.Infra.MongoDB.Abstractions;

public interface IMongoContext
{
    IMongoCollection<TDomain> GetCollection<TDomain>();
}