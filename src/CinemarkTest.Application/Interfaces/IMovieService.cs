using CinemarkTest.Domain.Models;

namespace CinemarkTest.Application.Interfaces;

public interface IMovieService
{
    Task<Movie> Create(Movie movie);

    Task<Movie> Update(Movie movie);
    Task Remove(Guid id);
    Task<IEnumerable<Movie>> GetAll();
}