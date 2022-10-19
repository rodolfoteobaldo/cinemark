namespace CinemarkTest.Domain.IntegrationEvents;

public class UpdatedMovieEvent : MovieEvent
{
    public UpdatedMovieEvent() : base(Subject)
    {
    }
    
    public new const string Subject = "cinemark.update.new.movie";
}