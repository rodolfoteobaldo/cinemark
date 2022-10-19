namespace CinemarkTest.Domain.IntegrationEvents;

public class CreatedMovieEvent : MovieEvent
{
    public CreatedMovieEvent() : base(Subject)
    {
    }
    
    public new const string Subject = "cinemark.create.new.movie";
}