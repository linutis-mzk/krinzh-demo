using System.Reflection;
using System.IO;
using Krinzh.Structs;
using Newtonsoft.Json.Linq;
using TwitchAPI.Structs;

public static class Settings
{
    private static readonly string settingsKeyName = "TwitchAPI";
    public static JToken settingsContentObj;
    private static JToken secretsContentObj;
    public static Delays Delays => GetDelays();
    public static PostgresCredentials PostgresCredentials => GetPostgresCredentials();
    public static TokenSettings TokenSettings => GetTokenSettings();
    public static string ConnectionString => GetConnectionString();

    static Settings()
    {

        string secretsFile = "secrets.json";
        string settingsFile = "settings.json";
        string workingDirectory = Environment.CurrentDirectory;
        
        string settingsAndSecretsLocation = Directory.GetParent(workingDirectory).FullName;
        string settingsPath = Path.Combine(settingsAndSecretsLocation, settingsFile);
        string secretsPath = Path.Combine(settingsAndSecretsLocation, secretsFile);
        
        /* Path inside docker container */
        string altSettingsPath = Path.Combine(settingsAndSecretsLocation, "settings", settingsFile);
        string altSecretsPath = "/run/secrets/postgres_secrets";
        
        #if !DEBUG
            secretsPath = altSecretsPath;
        #endif
        

        // Loading settings file
        Stream streamSettings = File.OpenRead(File.Exists(altSettingsPath) ? altSettingsPath : settingsPath);
        if (streamSettings == null)
        {
            throw new FileLoadException("Failed to load settings.json file.");
        }
        using (StreamReader reader = new StreamReader(streamSettings))
        {
            settingsContentObj = JObject.Parse(reader.ReadToEnd()).SelectToken(settingsKeyName);
        }
        
        // Loading secrets file
        Stream streamSecrets = File.OpenRead(File.Exists(altSecretsPath) ? altSecretsPath : secretsPath);
        if (streamSecrets == null)
        {
            throw new FileLoadException("Failed to load settings.json file.");
        }
        using (StreamReader reader = new StreamReader(streamSecrets))
        {
            secretsContentObj = JObject.Parse(reader.ReadToEnd()).SelectToken(settingsKeyName);
        }
    }

    private static TokenSettings GetTokenSettings()
    {
        TokenSettings tokenSettings = new TokenSettings();
        tokenSettings.tokenExpiryThreshold = settingsContentObj.SelectToken(
            "$.token-settings[?(@.id == 'token-expiry-threshold')].timespan").ToObject<int>();
        
        return tokenSettings;
    }
    
    private static Delays GetDelays()
    {
        Delays delays = new Delays();
        delays.apiCallStreamerstatus =
            (int)settingsContentObj.SelectToken(".delays-in-seconds[?(@.id == 'api-call-streamerstatus')].delay");
        delays.databaseGetStreamersList =
            (int)settingsContentObj.SelectToken(".delays-in-seconds[?(@.id == 'database-get-streamers')].delay");
        
        return delays;
    }
    
    private static PostgresCredentials GetPostgresCredentials()
    {
        PostgresCredentials credentials = new PostgresCredentials();
        credentials.user = secretsContentObj.SelectToken(
            "$.POSTGRES_USER").ToObject<string>();
        credentials.pass = secretsContentObj.SelectToken(
            "$.POSTGRES_PASS").ToObject<string>();

        return credentials;
    }
    
    private static string GetConnectionString()
    {
        return secretsContentObj.SelectToken("$.CONNECTION_STRING").ToObject<string>();
    }
    
    
}