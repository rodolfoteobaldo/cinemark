using CinemarkTest.Application.Interfaces;
using CinemarkTest.Application.Mappers;
using CinemarkTest.Application.Repositories;
using CinemarkTest.Domain.IntegrationEvents;
using CinemarkTest.Domain.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace CinemarkTest.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IRabbitMQProducer<CreatedMovieEvent> _producerCreatedMovie;
    private readonly IRabbitMQProducer<UpdatedMovieEvent> _producerUpdatedMovie;
    private readonly IRabbitMQProducer<DeletedMovieEvent> _producerDeletedMovie;

    public MovieService(IMovieRepository movieRepository,
        IRabbitMQProducer<CreatedMovieEvent> producerCreatedMovie,
        IRabbitMQProducer<UpdatedMovieEvent> producerUpdatedMovie,
        IRabbitMQProducer<DeletedMovieEvent> producerDeletedMovie)
    {
        _movieRepository = movieRepository;
        _producerCreatedMovie = producerCreatedMovie;
        _producerUpdatedMovie = producerUpdatedMovie;
        _producerDeletedMovie = producerDeletedMovie;
    }
    
    public async Task<Movie> Create(Movie movie)
    {
        var createdMovie = await _movieRepository.SaveAsync(movie);
        
        _producerCreatedMovie.Publish(movie.ToCreatedMovieEvent());
        
        return createdMovie;
    }

    public async Task<Movie> Update(Movie movie)
    {
        var updatedMovie = await _movieRepository.UpdateAsync(movie);
        
        _producerUpdatedMovie.Publish(movie.ToUpdatedMovieEvent());
        
        return updatedMovie;
    }

    public async Task Remove(Guid id)
    {
        await _movieRepository.DeleteOneAsync(id);
        _producerDeletedMovie.Publish(new DeletedMovieEvent
        {
            Id = id
        });
    }
    
    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await _movieRepository.GetAllAsync();
    }
}