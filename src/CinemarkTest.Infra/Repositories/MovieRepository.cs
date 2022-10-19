using CinemarkTest.Application.Repositories;
using CinemarkTest.Domain.Models;
using CinemarkTest.Infra.MongoDB.Abstractions;
using CinemarkTest.Infra.Redis;
using MongoDB.Driver;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace CinemarkTest.Infra.Repositories;

public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    private readonly IRedisDatabase _redisDatabase;

    public MovieRepository(IMongoContext mongoContext,
        IRedisDatabase redisDatabase) 
        : base(mongoContext)
    {
        _redisDatabase = redisDatabase;
    }
    
    public async Task<Movie> SaveAsync(Movie movie)
    {
        await Collection.InsertOneAsync(movie);
        return movie;
    }
    
    public Task<Movie> UpdateAsync(Movie movie)
    {
        var options = new FindOneAndReplaceOptions<Movie, Movie>
        {
            ReturnDocument = ReturnDocument.After
        };

        var filter = Builders<Movie>.Filter
            .Where(f => f.Id == movie.Id);

        return Collection.FindOneAndReplaceAsync(filter, movie, options);
    }
    
    public async Task DeleteOneAsync(Guid id)
        => await Collection.DeleteOneAsync(x => x.Id == id);

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _redisDatabase.GetOrSetAsync<IEnumerable<Movie>>("all_movies",
            async () =>
            {
                var moviesCursor = await Collection.FindAsync(_ => true);
                return await moviesCursor?.ToListAsync();
            },
            TimeSpan.FromSeconds(20));
    }
    
    public async Task<Movie> GetOneAsync(Guid id)
    {
        var filter = Builders<Movie>.Filter
            .Where(f => f.Id == id);
        
        var movieCursor = await Collection.FindAsync(filter);
        return await movieCursor.FirstOrDefaultAsync();
    }
}