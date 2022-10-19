using CinemarkTest.Application.Interfaces;
using CinemarkTest.Domain.Models;
using CinemarkTest.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemarkTest.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserViewModel userViewModel)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = userViewModel.UserName,
            Password = userViewModel.Password
        };
        return Ok(await _userService.Create(user));
    }
}