using System.Linq.Expressions;
using CinemarkTest.Domain.Models;

namespace CinemarkTest.Application.Repositories;

public interface IMovieRepository
{
    Task<Movie> SaveAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task DeleteOneAsync(Guid id);
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie> GetOneAsync(Guid id);
}