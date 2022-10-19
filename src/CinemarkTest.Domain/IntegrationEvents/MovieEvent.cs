namespace CinemarkTest.Domain.IntegrationEvents;

public class MovieEvent : Message
{
    public MovieEvent(string subject) : base(subject)
    {
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Synopsis { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int Runtime { get; set; }
}