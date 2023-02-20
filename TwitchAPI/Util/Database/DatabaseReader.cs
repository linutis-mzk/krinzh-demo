using System.Reflection;
using NodaTime;
using Npgsql;
using TwitchAPI.Models;

namespace TwitchAPI.Util;

public static class DatabaseReader
{

    public static string GetParameterClientId(NpgsqlConnection connection)
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM dbo.parameters WHERE pr_key='twitch_client_id'", connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        string clientId = string.Empty;
        while (reader.Read())
        {
            clientId = (string)reader[1];
        
        }
        reader.Close();
        return clientId;
    }
    public static string GetParameterClientSecret(NpgsqlConnection connection)
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM dbo.parameters WHERE pr_key='twitch_secret'", connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        string clientSecret = string.Empty;
        while (reader.Read())
        {
            clientSecret = (string)reader[1];
        
        }
        reader.Close();
        return clientSecret;
    }
    
    public static tblTokens GetAccessToken(NpgsqlConnection connection)
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM dbo.tokens WHERE owner='Twitch'", connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        tblTokens token = new tblTokens();
        while (reader.Read())
        {
            token.owner = (string)reader[0];
            token.time_updated = (DateTime)reader[1];
            token.access_token = (string)reader[2];
            token.expires_in = (int)reader[3];
            token.token_type = (string)reader[4];
        }
        reader.Close();
        return token;
    }
    
    /// <summary>
    /// Reads list streamers definition stored in the database.
    /// </summary>
    /// <returns>List of streamer IDs.</returns>
    public static List<string> GetLatestStreamersList(NpgsqlConnection connection)
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM dbo.streamers", connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        
        List<string> idList = new List<string>();
        
        while (reader.Read())
        {
            idList.Add((string)reader[0]);
        }
        reader.Close();
        return idList;
    }

    /// <summary>
    /// Check if connected database has correct tables.
    /// </summary>
    /// <param name="connection"></param>
    /// <returns>Will return true, if tables are correct</returns>
    public static bool VerifyDatabaseTables(NpgsqlConnection connection)
    {
        PropertyInfo[] propsStreamers = typeof(tblStreamers).GetProperties();
        PropertyInfo[] propsStreamersStatus = typeof(tblStreamersStatus).GetProperties();
        PropertyInfo[] propsTokens = typeof(tblTokens).GetProperties();
        
        List<string> streamersPropNames = propsStreamers.Select(p=>p.Name).ToList();
        List<string> statusPropNames = propsStreamersStatus.Select(p=>p.Name).ToList();
        List<string> tokenPropNames = propsTokens.Select(p=>p.Name).ToList();
        
        string query1 = "SELECT count(*) FROM information_schema.columns WHERE table_name = 'streamers' and column_name in "+Stringify<string>.ToTuple(streamersPropNames);
        string query2 = "SELECT count(*) FROM information_schema.columns WHERE table_name = 'streamers_status' and column_name in "+Stringify<string>.ToTuple(statusPropNames);
        string query3 = "SELECT count(*) FROM information_schema.columns WHERE table_name = 'tokens' and column_name in "+Stringify<string>.ToTuple(tokenPropNames);

        NpgsqlCommand command;
        NpgsqlDataReader reader;
        int result = 0;
        List<string> multipleQueries = new List<string>(){query1, query2, query3};
        foreach (var query in multipleQueries)
        {
            command = new NpgsqlCommand(query, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                result += Convert.ToInt32(reader[0]);
            }
            reader.Close();
        }
        
        return propsStreamers.Length + propsStreamersStatus.Length + propsTokens.Length == result;
    }
}