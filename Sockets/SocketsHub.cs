using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Sockets
{
    public class SocketsHub : Hub<SocketHubInterface>
    {
        public async Task GetLiveUpdatesAll(List<Stock> stocks)
        {
            Clients.All.GetLiveUpdatesAll(stocks);
        }


        public async Task AddMemberToStock(string stock  )
        {
            string connId = Context.ConnectionId;
            await Groups.AddToGroupAsync(connId, stock);
            await Clients.Client(connId).BucketAddAck("Added in Bucket");
        }

        public async Task BroadcastStockPriceHistory(Dictionary<string, List<int>> stockPriceHistory)
        {
            Clients.Group(stockPriceHistory.Keys.First()).BroadcastStockPriceHistory(stockPriceHistory);
            Clients.Groups(Context.ConnectionId);
        }

    }
}
