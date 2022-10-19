using CinemarkTest.Domain.Models;

namespace CinemarkTest.Application.Interfaces;

public interface IUserService
{
    Task<User> Create(User user);
    Task<User?> ValidateUser(User user);
}