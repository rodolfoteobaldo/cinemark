namespace CinemarkTest.Domain.IntegrationEvents;

public class DeletedMovieEvent : MovieEvent
{
    public DeletedMovieEvent() : base(Subject)
    {
    }
    
    public new const string Subject = "cinemark.delete.new.movie";
}