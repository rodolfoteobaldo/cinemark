namespace CinemarkTest.Domain.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Synopsis { get; set; }
    public int Rating { get; set; }
    public int Runtime { get; set; }
}