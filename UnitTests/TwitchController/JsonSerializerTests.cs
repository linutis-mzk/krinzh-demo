using Krinzh.Models;
using Krinzh.Util.JsonHelpers;
using Newtonsoft.Json;

namespace UnitTests;

public class JsonSerializerTests
{
    private bool consoleOutput = true;
    
    [SetUp]
    public void Setup()
    {
        
    }
    
    [Test]
    public void DeserializeAccessTokenResponse()
    {
        var json = @"{
            ""access_token"": ""2YotnFZFEjr1zCsicMWpAA"",
            ""token_type"": ""example"",
            ""expires_in"": 3600  }";

        MTwitchToken tokenInfo = TwitchSerializer.GetAppAccessToken(json);

        if (consoleOutput)
        {
            Console.WriteLine(tokenInfo.access_token);
            Console.WriteLine(tokenInfo.token_type);
            Console.WriteLine(tokenInfo.expires_in);
        }
        
        Assert.AreEqual("2YotnFZFEjr1zCsicMWpAA", tokenInfo.access_token);
        Assert.AreEqual("example", tokenInfo.token_type);
        Assert.AreEqual(3600, tokenInfo.expires_in);
    }
    [Test]
    public void DeserializeStreamerStatus()
    {
        var json = @"{
    ""data"": [
        {
            ""id"": ""41808645835"",
            ""user_id"": ""71092938"",
            ""user_login"": ""xqc"",
            ""user_name"": ""xQc"",
            ""game_id"": ""509658"",
            ""game_name"": ""Just Chatting"",
            ""type"": ""live"",
            ""title"": ""⚠️CLICK HERE⚠️DRAMA⚠️NEWS⚠️MASSIVE JUICE⚠️INCREDIBLE RIZZ⚠️WARLORD COMMANDER⚠️CHIEFTAIN⚠️GAMING GOD⚠️WOW⚠️CLICK HERE NOW⚠️INSANE⚠️WOW⚠️WOW⚠️"",
            ""viewer_count"": 82058,
            ""started_at"": ""2023-02-04T21:07:08Z"",
            ""language"": ""en"",
            ""thumbnail_url"": ""https://static-cdn.jtvnw.net/previews-ttv/live_user_xqc-{width}x{height}.jpg"",
            ""tag_ids"": [
                ""6ea6bca4-4712-4ab9-a906-e3336a9d8039"",
                ""c2839af5-f1d2-46c4-8edc-1d0bfbd85070""
            ],
            ""tags"": [
                ""English""
            ],
            ""is_mature"": false
        },
        {
            ""id"": ""41808842987"",
            ""user_id"": ""124422593"",
            ""user_login"": ""lec"",
            ""user_name"": ""LEC"",
            ""game_id"": ""21779"",
            ""game_name"": ""League of Legends"",
            ""type"": ""live"",
            ""title"": ""(Rebroadcast) 2023 LEC Winter - Week 3 Day 1"",
            ""viewer_count"": 6837,
            ""started_at"": ""2023-02-04T22:12:57Z"",
            ""language"": ""en"",
            ""thumbnail_url"": ""https://static-cdn.jtvnw.net/previews-ttv/live_user_lec-{width}x{height}.jpg"",
            ""tag_ids"": [
                ""6ea6bca4-4712-4ab9-a906-e3336a9d8039"",
                ""36a89a80-4fcd-4b74-b3d2-2c6fd9b30c95""
            ],
            ""tags"": [
                ""Esports"",
                ""English""
            ],
            ""is_mature"": false
        }
    ],
    ""pagination"": {
        ""cursor"": ""eyJiIjp7IkN1cnNvciI6ImV5SnpJam80TWpBMU9DNDVORFF3TVRVME1ESTFOeXdpWkNJNlptRnNjMlVzSW5RaU9uUnlkV1Y5In0sImEiOnsiQ3Vyc29yIjoiIn19""
    }
}";

        List<MStreamerStatus> statusList = TwitchSerializer.GetStreamersOnline(json);
        if (consoleOutput)
        {
            Console.WriteLine(statusList[0].user_id);
            Console.WriteLine(statusList[1].user_id);
            Console.WriteLine(statusList[0].stream_start_time);
        }
        Assert.NotNull(statusList[0].user_id);
        Assert.NotNull(statusList[1].user_id);
        Assert.NotNull(statusList[0].stream_start_time);
    }
}