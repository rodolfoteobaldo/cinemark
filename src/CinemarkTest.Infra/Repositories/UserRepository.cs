using CinemarkTest.Application.Repositories;
using CinemarkTest.Domain.Models;
using CinemarkTest.Infra.MongoDB.Abstractions;
using MongoDB.Driver;

namespace CinemarkTest.Infra.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(IMongoContext mongoContext) 
        : base(mongoContext)
    {
    }
    
    public async Task<User> SaveAsync(User user)
    {
        await Collection.InsertOneAsync(user);
        return user;
    }
    
    public async Task<User?> Get(User user)
    {
        var filter = Builders<User>.Filter
            .Where(f => f.UserName == user.UserName && f.Password == user.Password);

        using var entity = await Collection.FindAsync(filter);
        return entity?.FirstOrDefault();
    }
}