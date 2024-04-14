using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography.X509Certificates;
using System.Timers;

namespace Sockets.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class MainController : Controller
    {
        private IHubContext<SocketsHub, SocketHubInterface> _hubContext;
        private readonly TimerManager _timer;
        public MainController(IHubContext<SocketsHub, SocketHubInterface> hubContext, TimerManager timer  )
        {
            _hubContext = hubContext;
            _timer = timer;
        }

        [HttpGet]
        public IActionResult hello()
        {
            return Ok("Hello, Friend!");
        }

        [HttpGet] 
        public IActionResult InitialFill()
        {
            Random r = new Random();
            StockManager.Instance().AddStock("AAAA",r.Next(1,100));
            StockManager.Instance().AddStock("BBBB", r.Next(1, 100));
            StockManager.Instance().AddStock("CCCC", r.Next(1, 100));
            StockManager.Instance().AddStock("DDDD", r.Next(1, 100));
            StockManager.Instance().AddStock("EEEE", r.Next(1, 100));

            return Ok();
        }


        [HttpPost]
        public IActionResult GetAllStocks() 
        {
            var stocks = StockManager.Instance().GetAllStocks();
            _hubContext.Clients.All.GetLiveUpdatesAll(stocks);
            return Ok();
        }

        [HttpGet]
        public IActionResult StartStockTicker()
        {
            var stocks = StockManager.Instance().GetAllStocks();
            _hubContext.Clients.All.GetLiveUpdatesAll(stocks);
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer(() => _hubContext.Clients.All.GetLiveUpdatesAll(stocks));
            return Ok();
        }

        [HttpGet]
        public IActionResult StartPriceUpdate()
        {            
            System.Timers.Timer timerSet1 = new System.Timers.Timer(5000);
            timerSet1.Elapsed += (s, ee) => StockManager.Instance().UpdateStockPrices();
            timerSet1.Start();
            return Ok();
        }

        [HttpPost]
        public IActionResult StartPriceBroadcast()
        {
            System.Timers.Timer timerSet1 = new System.Timers.Timer(5000);
            timerSet1.Elapsed += (s, ee) => PriceBucketBroadcast();
            timerSet1.Start();
            return Ok();
        }

        [NonAction]
        public void PriceBucketBroadcast()
        {
            List<Stock> stocks = StockManager.Instance().GetAllStocks();
            foreach (Stock stock in stocks)
            {
                Dictionary<string, List<int>> stockPrices = new Dictionary<string, List<int>>();
                stockPrices[stock.Name] = StockManager.Instance().GetStockPriceHistory(stock.Name);
                _hubContext.Clients.Group(stock.Name).BroadcastStockPriceHistory(stockPrices);

            }
        }

    }
}
