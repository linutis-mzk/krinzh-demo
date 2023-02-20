using System.Reflection;
using Krinzh.Models;
using NodaTime;
using Npgsql;
using TwitchAPI.Models;

namespace TwitchAPI.Util;

public static class DatabaseReaderAsync
{
    private static readonly NpgsqlConnection connection;

    static DatabaseReaderAsync()
    {
        NpgsqlConnectionStringBuilder connStringBuilder = new NpgsqlConnectionStringBuilder(Settings.ConnectionString);
        connStringBuilder.Username = Settings.PostgresCredentials.user;
        connStringBuilder.Password = Settings.PostgresCredentials.pass;
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connStringBuilder.ConnectionString);
        var dataSource = dataSourceBuilder.Build();
        
        connection = dataSource.OpenConnection();
    }
    
    public static async IAsyncEnumerable<MStreamerCard> GetStreamerCardsAsync()
    {
        // TODO: Then send-request in TwitchHub should be displaying all sent data.
        
        
        NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM dbo.streamers_status ORDER BY time_updated DESC LIMIT @streamerCount", connection);
        command.Parameters.AddWithValue("streamerCount", DatabaseCache.Streamers.Count);
        NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        MStreamerCard card;
        
        while (await reader.ReadAsync())
        {
            MStreamer streamer = DatabaseCache.Streamers.Find(x => x.user_id == (string)reader[1]);
            card = DatabaseCache.StreamerCards.Find(x => x.user_name == streamer.user_name);
            card.time_updated = (DateTime)reader[0];
            card.is_online = (bool)reader[4];
            yield return card;
        }
        reader.Close();
    }
}