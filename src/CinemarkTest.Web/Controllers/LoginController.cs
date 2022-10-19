using CinemarkTest.Application.Interfaces;
using CinemarkTest.Domain.Models;
using CinemarkTest.Web.Token;
using CinemarkTest.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CinemarkTest.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtSettings _settings;

    public LoginController(IUserService userService,
        IOptions<JwtSettings> options)
    {
        _userService = userService;
        _settings = options.Value;
    }

    [HttpPost]
    public async Task<ActionResult<TokenViewModel>> Post(UserViewModel userViewModel)
    {
        var user = await _userService.ValidateUser(new User
        {
            UserName = userViewModel.UserName,
            Password = userViewModel.Password
        });
        
        if (user == null)
            return BadRequest("Invalid credentials");

        var token = ValidateUserToken.GetToken(user, _settings);
        
        return Ok(new TokenViewModel
        {
            AccessToken = token
        });
    }
}