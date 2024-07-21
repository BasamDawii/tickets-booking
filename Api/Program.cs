using System.Reflection;
using Api.State;
using Core.Interfaces;
using Core.Models;
using Fleck;
using lib;
using Service;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new Ticket
{
    TotalTickets = 100,
    TicketsSold = 0,
    BasePrice = 50.0,
    PriceIncreasePerTicket = 2.0
});

builder.Services.AddSingleton<ITicketService, TicketService>();
builder.Services.AddSingleton<IWebSocketConnectionManager, WebSocketConnectionManager>();


var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());
var app = builder.Build();
var server = new WebSocketServer("ws://0.0.0.0:8181");


server.Start(socket =>
{
    socket.OnOpen = () => Console.WriteLine("Open!");
    socket.OnClose = () => Console.WriteLine("Close!");
    socket.OnMessage = async message =>
    {
        try
        {
            await app.InvokeClientEventHandler(clientEventHandlers, socket, message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    };
});


app.Run();