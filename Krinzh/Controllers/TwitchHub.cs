using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Krinzh.Models;
using Microsoft.AspNetCore.SignalR;
using TwitchAPI.Util;

public class TwitchHub : Hub
{
    public async IAsyncEnumerable<MStreamerCard> GetStreamers(
        [EnumeratorCancellation]
        CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await foreach (var item in DatabaseReaderAsync.GetStreamerCardsAsync())
            {
                yield return item;
            }
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        }
    }
}