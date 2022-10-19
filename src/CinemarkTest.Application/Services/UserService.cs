using CinemarkTest.Application.Interfaces;
using CinemarkTest.Application.Repositories;
using CinemarkTest.Domain.Models;

namespace CinemarkTest.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Create(User user)
    {
        return await _userRepository.SaveAsync(user);
    }

    public async Task<User?> ValidateUser(User user)
    {
        if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.UserName))
            return default;

        var validatedUser = await _userRepository.Get(user);
        return validatedUser ?? default;
    }
}