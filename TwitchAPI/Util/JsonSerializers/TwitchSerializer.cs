using System.Net.Http.Json;
using TwitchAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using TwitchAPI.Models;

namespace TwitchAPI.Util.JsonSerializers;

/// <summary>
/// Helper class used for serializing JSON data into their respective modules.
/// Jsons are received as responses as string from Twitch API.
/// </summary>
public static class TwitchSerializer
{
    public static T? Deserialize<T>(string? response)
    {
        try
        {
            T obj = JsonConvert.DeserializeObject<T>(response);
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred trying to deserialize JSON of type " + typeof(T).ToString() + ".");
        }
        return JsonConvert.DeserializeObject<T>(response);
    }
    public static List<tblStreamersStatus> GetStreamersStatusTable(List<string> streamerIds, MStreamers streamersOnline)
    {
        if (streamerIds is null || streamersOnline is null)
        {
            return null;
        }

        List<tblStreamersStatus> table = new List<tblStreamersStatus>();
        tblStreamersStatus row;
        List<string> processedIds = new List<string>();
        foreach (var streamer in streamersOnline.data)
        {
            row = new tblStreamersStatus();
            row.time_updated = DateTime.Now;
            row.user_id = streamer.user_id;
            row.game_id = streamer.game_id;
            row.viewer_count = streamer.viewer_count;
            row.is_online = true;
            row.stream_start_time = streamer.started_at;
            row.stream_end_time = DateTime.MinValue;
            processedIds.Add(streamer.user_id);
            table.Add(row);
        }
        foreach (var id in streamerIds)
        {
            if (!processedIds.Contains(id))
            {
                row = new tblStreamersStatus();
                row.time_updated = DateTime.Now;
                row.user_id = id;
                row.game_id = "";
                row.viewer_count = 0;
                row.is_online = false;
                row.stream_start_time = DateTime.MinValue;
                row.stream_end_time = DateTime.MinValue;
                table.Add(row);
            }
        }

        return table;
    }
    public static List<tblStreamers> GetStreamersTable(MUsers userInfo)
    {
        List<tblStreamers> table = new List<tblStreamers>();
        tblStreamers row = new tblStreamers();

        foreach (var user in userInfo.data)
        {
            row.user_id = user.id;
            row.user_name = user.login;
            row.display_name = user.display_name;
            row.custom_alias = "";
            row.profile_image_url = user.profile_image_url;
            table.Add(row);
        }

        return table;
    }
}