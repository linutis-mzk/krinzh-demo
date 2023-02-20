using Krinzh.Models;
using TwitchAPI.Models;

namespace TwitchAPI.Util;

/// <summary>
/// Load data that does not change, yet is accessed frequently.
/// </summary>
public static class DatabaseCache
{
    public static List<MStreamer> Streamers { get => _streamers; }
    private static List<MStreamer> _streamers { get; set; }
    
    public static List<MStreamerCard> StreamerCards { get => _streamersCards; }
    private static List<MStreamerCard> _streamersCards { get; set; }


    static DatabaseCache()
    {
        _streamers = DatabaseReader.GetStreamers();
        _streamersCards = DatabaseReader.GetStreamerCards();
    }
}