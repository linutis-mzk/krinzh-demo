using System.Net;

namespace TwitchAPI;

public class Authentication : TwitchBase
{
    public static async Task<string> GetAccessToken(string clientId, string secret)
    {
        string uri = "https://id.twitch.tv/oauth2/token";
        Dictionary<string, string> postParams = new Dictionary<string, string>
        {
            { "client_id", clientId }, 
            { "client_secret", secret },
            { "grant_type", "client_credentials" }
        };
        var encodedContent = new FormUrlEncodedContent (postParams);
        
        var response = await Client.PostAsync(uri, encodedContent);
        if (response.StatusCode == HttpStatusCode.OK) {
            // Do something with response. Example get content:
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        return string.Empty;
    }
}