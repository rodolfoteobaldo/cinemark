namespace CinemarkTest.Domain.IntegrationEvents;

public abstract class Message
{
    protected Message(string subject)
    {
        Subject = subject;
    }
    public string Subject { get; set; }
}