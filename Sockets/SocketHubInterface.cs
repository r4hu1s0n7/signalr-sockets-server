namespace Sockets
{
    public interface SocketHubInterface
    {
        Task GetLiveUpdatesAll(List<Stock> stocks);
        Task BucketAddAck(string message);
        Task BroadcastStockPriceHistory(Dictionary<string, List<int>> stockPriceHistory);
    }
}
