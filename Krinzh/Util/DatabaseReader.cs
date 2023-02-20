using System.Reflection;
using Krinzh.Models;
using NodaTime;
using Npgsql;
using TwitchAPI.Models;

namespace TwitchAPI.Util;

public static class DatabaseReader
{
    private static readonly NpgsqlConnection connection;

    static DatabaseReader()
    {
        NpgsqlConnectionStringBuilder connStringBuilder = new NpgsqlConnectionStringBuilder(Settings.ConnectionString);
        connStringBuilder.Username = Settings.PostgresCredentials.user;
        connStringBuilder.Password = Settings.PostgresCredentials.pass;
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connStringBuilder.ConnectionString);
        var dataSource = dataSourceBuilder.Build();
        
        connection = dataSource.OpenConnection();
    }
    
    #region Cache Loader Functions
    public static List<MStreamer> GetStreamers()
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM dbo.streamers", connection);
        NpgsqlDataReader reader = command.ExecuteReader();

        MStreamer streamer;
        List<MStreamer> streamers = new List<MStreamer>();
        while (reader.Read())
        {
            streamer = new MStreamer();
            streamer.user_id = (string)reader[0];
            streamer.user_name = (string)reader[1];
            streamer.display_name = (string)reader[2];
            streamer.custom_alias = (string)reader[3];
            streamer.profile_image_url = (string)reader[4];
            streamers.Add(streamer);
        }
        reader.Close();
        return streamers;
    }
    public static List<MStreamerCard> GetStreamerCards()
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM dbo.streamers_status ORDER BY time_updated LIMIT @streamerCount", connection);
        command.Parameters.AddWithValue("streamerCount", DatabaseCache.Streamers.Count);
        NpgsqlDataReader reader = command.ExecuteReader();

        MStreamerCard card;
        MStreamer streamer;
        List<MStreamerCard> cardList = new List<MStreamerCard>();
        while (reader.Read())
        {
            streamer = DatabaseCache.Streamers.Find(x => x.user_id == (string)reader[1]);
            card = new MStreamerCard();
            
            card.time_updated = (DateTime)reader[0];
            card.user_name = streamer.user_name;
            card.is_online = (bool)reader[4];
            card.custom_alias = streamer.custom_alias;
            card.profile_image_url = streamer.profile_image_url;
            
            cardList.Add(card);
        }
        reader.Close();
        return cardList;
    }
    #endregion
}