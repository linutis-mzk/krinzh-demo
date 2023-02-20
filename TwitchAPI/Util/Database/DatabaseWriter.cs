using Npgsql;
using NpgsqlTypes;
using TwitchAPI.Models;

namespace TwitchAPI.Util;

public static class DatabaseWriter
{
    public static async void InsertStreamers(List<tblStreamers> streamers, NpgsqlConnection connection)
    {
        string sqlFlush = "TRUNCATE FROM dbo.streamers";
        NpgsqlCommand command = new NpgsqlCommand(sqlFlush, connection);
        command.ExecuteNonQuery();
        
        foreach (tblStreamers streamer in streamers)
        {
            string sqlQuery = @"
                INSERT INTO dbo.streamers_status (time_updated, user_id, game_id, viewer_count, is_online, stream_start_time, stream_end_time ) 
                VALUES (@time_updated, @user_id, @game_id, @viewer_count, @is_online, @stream_start_time, @stream_end_time)";
            
            command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@user_id", streamer.user_id);
            command.Parameters.AddWithValue("@user_name", streamer.user_name);
            command.Parameters.AddWithValue("@display_name", streamer.display_name);
            command.Parameters.AddWithValue("@custom_alias", streamer.custom_alias);
            command.Parameters.AddWithValue("@profile_image_url", streamer.profile_image_url);
            await command.ExecuteNonQueryAsync();
        }
    }
    public static async void InsertStreamersStatus(List<tblStreamersStatus> statusList, NpgsqlConnection connection)
    {
        foreach (tblStreamersStatus status in statusList)
        {
            string sqlQuery = @"
                INSERT INTO dbo.streamers_status (time_updated, user_id, game_id, viewer_count, is_online, stream_start_time, stream_end_time ) 
                VALUES (@time_updated, @user_id, @game_id, @viewer_count, @is_online, @stream_start_time, @stream_end_time)";
            
            NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@time_updated", NpgsqlDbType.Timestamp, status.time_updated);
            command.Parameters.AddWithValue("@user_id", status.user_id);
            command.Parameters.AddWithValue("@game_id", status.game_id);
            command.Parameters.AddWithValue("@viewer_count", status.viewer_count);
            command.Parameters.AddWithValue("@is_online", status.is_online);
            command.Parameters.AddWithValue("@stream_start_time", status.stream_start_time);
            command.Parameters.AddWithValue("@stream_end_time", status.stream_end_time);
            await command.ExecuteNonQueryAsync();
        }
    }
    
    public static async void UpdateAccessToken(MAccessToken token, NpgsqlConnection connection)
    {

        string sqlQuery = @"
                UPDATE dbo.tokens SET time_updated=@time_updated, access_token=@access_token, expires_in=@expires_in, token_type=@token_type WHERE owner=@owner;";
        
            NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@owner","Twitch");
            command.Parameters.AddWithValue("@time_updated", NpgsqlDbType.Timestamp, DateTime.Now);
            command.Parameters.AddWithValue("@access_token", token.access_token);
            command.Parameters.AddWithValue("@expires_in", token.expires_in);
            command.Parameters.AddWithValue("@token_type", token.token_type);
            await command.ExecuteNonQueryAsync();
            

    }
}