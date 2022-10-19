using CinemarkTest.Infra.MongoDB.Abstractions;
using MongoDB.Driver;

namespace CinemarkTest.Infra.Repositories;

public abstract class RepositoryBase<TDomain>
{
    protected readonly IMongoCollection<TDomain> Collection;

    protected RepositoryBase(IMongoContext mongoContext) => 
        Collection = mongoContext.GetCollection<TDomain>();
}