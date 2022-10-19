using CinemarkTest.Domain.IntegrationEvents;
using CinemarkTest.Domain.Models;

namespace CinemarkTest.Application.Mappers;

public static class MovieMapper
{
    public static CreatedMovieEvent ToCreatedMovieEvent(this Movie movie) =>
        new()
        {
            Id = movie.Id,
            Name = movie.Name,
            Rating = movie.Rating,
            Runtime = movie.Runtime,
            Synopsis = movie.Synopsis
        };
    
    public static UpdatedMovieEvent ToUpdatedMovieEvent(this Movie movie) =>
        new()
        {
            Id = movie.Id,
            Name = movie.Name,
            Rating = movie.Rating,
            Runtime = movie.Runtime,
            Synopsis = movie.Synopsis
        };
}