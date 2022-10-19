using CinemarkTest.Domain.Models;

namespace CinemarkTest.Application.Repositories;

public interface IUserRepository
{
    Task<User> SaveAsync(User user);
    Task<User?> Get(User user);
}