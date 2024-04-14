# signalr-sockets-server

## Controller API
```api/hello```
to test the app.

```api/InitialFill```
it creates 5 stocks and adds random price

```api/GetAllStocks```
it triggers broadcast of all stocks to all clients.
API here is not returning the response, rather we are using it to trigger socket communication.

```api/StartStockTicker```
it starts times to broadcast stocks every 5 seconds to all the connected clients

```api/StartPriceUpdate```
it starts updating the price of stocks with slight manipulation, to create realtime stock update every 5 seconds

```api/StartPriceBroadcast```
this starts broadcasting stock prices to buckets, if there are any.
bucket here is similar to any virtual collection of stocks, like portfolio, watchlist or mutualfund.
here assume group key is stock name, and value is list of users that are subscribed to that group, number of stock = number of groups. and we maintain the list of subscribers by making them part of group.


## Socket Endpoint ##
```AddMemberToStock```
Any caller will be added to group, only need to pass stock name and connection Id will be taken implicitly from connection context
