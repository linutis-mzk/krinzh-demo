using System.Reflection;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using TwitchAPI;
using TwitchAPI.Models;


//TODO: Load everything from database at the start, then store new state back to the database at the end.
//TODO: Verify database schema.
// i.e. load streamers, token etc.

// Currently: no token is being retrieved and saved into database, needs to be fixed.



var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<StreamerStatusTracker>();
    })
    .Build();


await host.RunAsync();