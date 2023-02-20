using System.Net;
using System.Text;



namespace TwitchAPI;

public class Streamers : TwitchBase
{
    public static async Task<string> GetStreamersStatus(string clientId, string token, string[] userIdArray)
    {
        StringBuilder uriBase = new StringBuilder("https://api.twitch.tv/helix/streams?");
        foreach (string id in userIdArray)
        {
            uriBase.Append($"user_id={id}&");
        }

        uriBase.Length--;
        string uri = uriBase.ToString(); // removes trailing & for clean uri
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
        
        
        request.Headers.Add("Client-Id", clientId);
        request.Headers.Add("Authorization", "Bearer "+token);

        HttpResponseMessage? response = null;
        try
        {
            response = await Client.SendAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        
        if (response != null && response.StatusCode == HttpStatusCode.OK) {
            // Do something with response. Example get content:
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        return string.Empty;
    }
    public static async Task<string> GetStreamersMetadata(string clientId, string token, string[] userIdArray)
    {
        StringBuilder uriBase = new StringBuilder("https://api.twitch.tv/helix/users?");
        foreach (string id in userIdArray)
        {
            uriBase.Append($"id={id}&");
        }

        uriBase.Length--;
        string uri = uriBase.ToString(); // removes trailing & for clean uri
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
        
        request.Headers.Add("Client-Id", clientId);
        request.Headers.Add("Authorization", "Bearer "+token);
        
        HttpResponseMessage? response = null;
        try
        {
            response = await Client.SendAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
  
        if (response.StatusCode == HttpStatusCode.OK) {
            // Do something with response. Example get content:
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        return string.Empty;
    }
}