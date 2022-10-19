using AutoBogus;
using CinemarkTest.Domain.Models;
using CinemarkTest.Infra.MongoDB;
using CinemarkTest.Infra.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Mongo2Go;
using Moq;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Xunit;

namespace CinemarkTests.UnitTests.Infra;

public class MovieRepositoryTests
{
    private readonly MovieRepository _repository;
    private readonly Mock<IRedisDatabase> _mockRedisDatabase;
    private readonly MongoDbRunner Runner;
    private readonly IOptions<MongoDbSettings> _mongoDbSettings;
    
    public MovieRepositoryTests()
    {
        Runner = MongoDbRunner.Start();
        
        var mongoSettings = new MongoDbSettings
        {
            ConnectionString = Runner.ConnectionString,
            DatabaseName = "Cinemark"
        };
        
        _mongoDbSettings = Options.Create(mongoSettings);

        _mockRedisDatabase = new Mock<IRedisDatabase>();

        _repository = new MovieRepository(new MongoContext(_mongoDbSettings), _mockRedisDatabase.Object);
    }

    [Fact]
    public async Task SaveAsync_Should_Success()
    {
        var movie = AutoFaker.Generate<Movie>();

        var result = await _repository.SaveAsync(movie);

        result.Should().NotBeNull();

        var resultMovie = await _repository.GetOneAsync(movie.Id);
        resultMovie.Should().NotBeNull();
    }
    
    [Fact]
    public async Task UpdateAsync_Should_Success()
    {
        var movie = AutoFaker.Generate<Movie>();

        var result = await _repository.SaveAsync(movie);
        result.Should().NotBeNull();

        movie.Runtime = 100;
        var movieUpdated = await _repository.UpdateAsync(movie);

        movieUpdated.Runtime.Should().Be(100);
    }
    
    [Fact]
    public async Task DeleteAsync_Should_Success()
    {
        var movie = AutoFaker.Generate<Movie>();

        var result = await _repository.SaveAsync(movie);
        result.Should().NotBeNull();

        await _repository.DeleteOneAsync(movie.Id);
        
        var resultMovie = await _repository.GetOneAsync(movie.Id);
        resultMovie.Should().BeNull();
    }
}