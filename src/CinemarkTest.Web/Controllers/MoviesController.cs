using CinemarkTest.Application.Interfaces;
using CinemarkTest.Domain.Models;
using CinemarkTest.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemarkTest.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Movie>> Get()
    {
        return await _movieService.GetAll();
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]MovieViewModel movieViewModel)
    {
        var movie = new Movie
        {
            Name = movieViewModel.Name,
            Rating = movieViewModel.Rating,
            Runtime = movieViewModel.Runtime,
            Synopsis = movieViewModel.Synopsis
        };
        return Ok(await _movieService.Create(movie));
    }
    
    [HttpPut]
    public async Task<IActionResult> Put([FromBody]MovieViewModel movieViewModel)
    {
        var movie = new Movie
        {
            Id = movieViewModel.Id,
            Name = movieViewModel.Name,
            Rating = movieViewModel.Rating,
            Runtime = movieViewModel.Runtime,
            Synopsis = movieViewModel.Synopsis
        };
        return Ok(await _movieService.Update(movie));
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _movieService.Remove(id);
        return Ok();
    }
}