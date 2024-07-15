using System.Reflection;
using Fleck;
using lib;


var builder = WebApplication.CreateBuilder(args);
var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());
var app = builder.Build();


int totalTickets = 100; 
int ticketsSold = 0;
double basePrice = 50.0; 
double priceIncreasePerTicket = 2.0; 
List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();


var server = new WebSocketServer("ws://0.0.0.0:8181");

server.Start(socket =>
{
    socket.OnOpen = () =>
    {
        Console.WriteLine("Open!");
        allSockets.Add(socket);
        SendPriceUpdate(socket);
    };
    socket.OnClose = () =>
    {
        Console.WriteLine("Close!");
        allSockets.Remove(socket);
    };
    socket.OnMessage = message =>
    {
        Console.WriteLine(message);
        if (message == "buy")
        {
            BuyTicket();
            BroadcastPriceUpdate();
        }
    };
});

Console.WriteLine("Press any key to exit...");
Console.ReadKey();


void BuyTicket()
{
    if (ticketsSold < totalTickets)
    {
        ticketsSold++;
        Console.WriteLine($"Ticket sold! Total sold: {ticketsSold}");
    }
}


void BroadcastPriceUpdate()
{
    var currentPrice = CalculateCurrentPrice();
    foreach (var socket in allSockets)
    {
        socket.Send($"Current Price: {currentPrice:C}");
    }
}


double CalculateCurrentPrice()
{
    return basePrice + (ticketsSold * priceIncreasePerTicket);
}


void SendPriceUpdate(IWebSocketConnection socket)
{
    var currentPrice = CalculateCurrentPrice();
    socket.Send($"Current Price: {currentPrice:C}");
}

app.Run();