namespace Sockets
{
    public class StockManager
    {
        private static StockManager _instance;
        List<Stock> stocks = new List<Stock>();
        Dictionary<string,List<int>> priceHistory = new Dictionary<string,List<int>>();

        public static StockManager Instance()
        {
            if(_instance == null) _instance = new StockManager();
            return _instance;
        }

        public void AddStock(string name, int price)
        {
            stocks.Add(new Stock(name, price));
            priceHistory[name] = new List<int>();
            priceHistory[name].Add(price);
        }

        public List<Stock> GetAllStocks()
        {
            return stocks;
        }

        public void UpdateStockPrices() 
        {
            foreach(var s in stocks)
            {
                int newPrice = UpdatePrice(s.Price);
                s.Price = newPrice;
                priceHistory[s.Name].Add(newPrice);
            }
            Console.WriteLine(priceHistory["AAAA"].Count);
        }

        public List<int> GetStockPriceHistory(string name)
        {
            return priceHistory[name];
        }
        private int UpdatePrice(int price)
        {
            Random random = new Random();
            int varaition = random.Next(-3,3);
            int newPrice = price + varaition;
            return newPrice > 0 ? newPrice : 1;
        }

        
    }
}
