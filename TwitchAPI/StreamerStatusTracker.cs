using System.Text;
using Npgsql;
using System.Data.SqlClient;
using System.Text.Json;
using Krinzh.Structs;
using NodaTime;
using NpgsqlTypes;
using TwitchAPI.Models;
using TwitchAPI.Structs;
using TwitchAPI.Util;
using TwitchAPI.Util.JsonSerializers;


namespace TwitchAPI;

public sealed class StreamerStatusTracker : BackgroundService
{
    
    private readonly ILogger<StreamerStatusTracker> _logger;
    private readonly NpgsqlConnection connection;
    private readonly PostgresCredentials databaseCredentials;
    private readonly DateTime startTime;
    private readonly Delays delays;

    private TimeSpan runtime;
    private List<string> streamersList;
    private string access_token;
    private string[] twitchClientCredentials;


    public StreamerStatusTracker(ILogger<StreamerStatusTracker> logger)
    {
        _logger = logger;
        startTime = DateTime.Now;
        
        // Building a database connection
        databaseCredentials = Settings.PostgresCredentials;
        NpgsqlConnectionStringBuilder connStringBuilder = new NpgsqlConnectionStringBuilder(Settings.ConnectionString);
        connStringBuilder.Username = databaseCredentials.user;
        connStringBuilder.Password = databaseCredentials.pass;
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connStringBuilder.ConnectionString);
        var dataSource = dataSourceBuilder.Build();
        connection = dataSource.OpenConnection();
        
        delays = Settings.Delays;
        twitchClientCredentials = new[]
        {
            DatabaseReader.GetParameterClientId(connection),
            DatabaseReader.GetParameterClientSecret(connection)
        };
        
        
        // Acquiring token
        string response = string.Empty;
        tblTokens token = DatabaseReader.GetAccessToken(connection);
        access_token = token.access_token;
        TimeSpan tokenExpiryActual = token.time_updated - token.time_updated.AddSeconds(token.expires_in);
        if (tokenExpiryActual.TotalSeconds < Settings.TokenSettings.tokenExpiryThreshold)
        {
            response = Authentication.GetAccessToken(twitchClientCredentials[0], twitchClientCredentials[1]).Result;
            MAccessToken newToken = TwitchSerializer.Deserialize<MAccessToken>(response);
            DatabaseWriter.UpdateAccessToken(newToken, connection);
            access_token = newToken.access_token;
        }

        
            
        if (string.IsNullOrEmpty(access_token))
        {
            _logger.LogError("Could not attain access token. Please check your client ID and secret.");
        }
     

        //!DatabaseReader.VerifyDatabaseTables(connection);
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //TODO: Improve tables verification function.
        //TODO: Add logic to check if token is expired and request new one.
        //TODO: Add logic to check when the stream ended.
        //TODO: Add data to temporary table, then save state for some granularity
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Runtime: Days {0} {1}:{2}:{3}", runtime.Days, runtime.Hours, runtime.Minutes, runtime.Seconds);
            
            // Check if list of streamers of interest has changed.
            if (runtime.TotalSeconds % delays.databaseGetStreamersList == 0)
            {
                streamersList = DatabaseReader.GetLatestStreamersList(connection);
                _logger.LogInformation("Reading existing streamers, found: {0}", streamersList.Count);
            }
            
            if (streamersList.Count > 0)
            {
                string rawResponse = Streamers.GetStreamersStatus(twitchClientCredentials[0], access_token, streamersList.ToArray()).Result;
                if (rawResponse.Length > 0)
                {
                    
                    MStreamers status = TwitchSerializer.Deserialize<MStreamers>(rawResponse);
                    List<tblStreamersStatus> table = TwitchSerializer.GetStreamersStatusTable(streamersList, status);
                    DatabaseWriter.InsertStreamersStatus(table, connection);
                    _logger.LogInformation("New status received. Added {0} new rows.", table.Count);
                }
            }
            
            runtime = DateTime.Now - startTime;
            await Task.Delay(TimeSpan.FromSeconds(delays.apiCallStreamerstatus), stoppingToken);
        }
    }
}