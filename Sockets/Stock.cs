namespace Sockets
{
    public class Stock
    {
        public int Price { get; set; }

        public string Name { get; set; }

        public Stock(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
//Guidelines
/*
 * Create a normal default api ticking top 5 main stocks
 * everyone subscribed to it by default, every client connected
 * 
 * Bucket option and channel subscried, think more on how to implement, watch list feature
 * 
 * Flush 1 minute queue date in csv
 */