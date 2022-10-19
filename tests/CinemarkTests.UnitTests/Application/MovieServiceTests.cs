using AutoBogus;
using CinemarkTest.Application.Repositories;
using CinemarkTest.Application.Services;
using CinemarkTest.Domain.IntegrationEvents;
using CinemarkTest.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace CinemarkTests.UnitTests.Application;

public class MovieServiceTests
{
    private readonly Mock<IMovieRepository> _mockMovieRepository;
    private readonly Mock<IRabbitMQProducer<CreatedMovieEvent>> _mockProducerCreatedMovie;
    private readonly Mock<IRabbitMQProducer<UpdatedMovieEvent>> _mockProducerUpdatedMovie;
    private readonly Mock<IRabbitMQProducer<DeletedMovieEvent>> _mockProducerDeletedMovie;
    private readonly MovieService _service;
    
    public MovieServiceTests()
    {
        _mockMovieRepository = new Mock<IMovieRepository>();
        _mockProducerCreatedMovie = new Mock<IRabbitMQProducer<CreatedMovieEvent>>();
        _mockProducerUpdatedMovie = new Mock<IRabbitMQProducer<UpdatedMovieEvent>>();
        _mockProducerDeletedMovie = new Mock<IRabbitMQProducer<DeletedMovieEvent>>();
        
        _service = new MovieService(
            _mockMovieRepository.Object,
            _mockProducerCreatedMovie.Object,
            _mockProducerUpdatedMovie.Object,
            _mockProducerDeletedMovie.Object
        );
    }

    [Fact]
    public async Task CreateMovie_ShouldReturn_Success()
    {
        var movie = AutoFaker.Generate<Movie>();

        _mockMovieRepository.Setup(x => x.SaveAsync(It.IsAny<Movie>()))
            .ReturnsAsync(movie);

        _mockProducerCreatedMovie.Setup(x => x.Publish(It.IsAny<CreatedMovieEvent>()));
        
        var result = await _service.Create(movie);

        result.Id.Should().Be(movie.Id);
        result.Name.Should().Be(movie.Name);
        
        _mockMovieRepository.Verify(x => x.SaveAsync(It.IsAny<Movie>()), Times.Once);
        _mockProducerCreatedMovie.Verify(x => x.Publish(It.IsAny<CreatedMovieEvent>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateMovie_ShouldReturn_Success()
    {
        var movie = AutoFaker.Generate<Movie>();

        _mockMovieRepository.Setup(x => x.UpdateAsync(It.IsAny<Movie>()))
            .ReturnsAsync(movie);

        _mockProducerUpdatedMovie.Setup(x => x.Publish(It.IsAny<UpdatedMovieEvent>()));
        
        var result = await _service.Update(movie);

        result.Id.Should().Be(movie.Id);
        result.Name.Should().Be(movie.Name);
        
        _mockMovieRepository.Verify(x => x.UpdateAsync(It.IsAny<Movie>()), Times.Once);
        _mockProducerUpdatedMovie.Verify(x => x.Publish(It.IsAny<UpdatedMovieEvent>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteMovie_ShouldReturn_Success()
    {
        _mockMovieRepository.Setup(x => x.DeleteOneAsync(It.IsAny<Guid>()));

        _mockProducerDeletedMovie.Setup(x => x.Publish(It.IsAny<DeletedMovieEvent>()));
        
        Func<Task> result = async () => await _service.Remove(Guid.NewGuid());
        await result.Should().NotThrowAsync();
        
        _mockMovieRepository.Verify(x => x.DeleteOneAsync(It.IsAny<Guid>()), Times.Once);
        _mockProducerDeletedMovie.Verify(x => x.Publish(It.IsAny<DeletedMovieEvent>()), Times.Once);
    }
    
    [Fact]
    public async Task GetAllMovies_ShouldReturn_Success()
    {
        var movies = AutoFaker.Generate<Movie>(3);

        _mockMovieRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(movies);

        var result = await _service.GetAll();

        result.Count().Should().BeGreaterThan(0);

        _mockMovieRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}