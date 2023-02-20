using System.Reflection;
using Krinzh.Structs;
using Newtonsoft.Json.Linq;

public static class Settings
{
    private static readonly string settingsKeyName = "Krinzh";
    public static PostgresCredentials PostgresCredentials => GetPostgresCredentials();
    public static string ConnectionString => GetConnectionString();
    public static Delays Delays => GetDelays();
    
    
    private static JToken settingsContentObj;
    private static JToken secretsContentObj;
    
    
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
                settingsPath = altSettingsPath;
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
    
    private static Delays GetDelays()
    {
        Delays delays = new Delays();
        delays.databaseGetStreamerStatus =
            (int)settingsContentObj.SelectToken(".delays-in-seconds[?(@.id == 'database-get-streamerstatus')].delay");

        return delays;
    }
}