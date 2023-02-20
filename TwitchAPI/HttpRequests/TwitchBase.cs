namespace TwitchAPI;

public class TwitchBase
{
    protected static readonly HttpClient Client = new HttpClient();

    protected TwitchBase()
    {
        Client.Timeout = TimeSpan.FromSeconds(10);
    }
}