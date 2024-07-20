using System.Reflection;
using Fleck;
using lib;
using Service;
using tickets_booking;


    var builder = WebApplication.CreateBuilder(args);
    var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());
    builder.Services.AddSingleton<TicketService>();
    var app = builder.Build();

    var server = new WebSocketServer("ws://0.0.0.0:8181");

    server.Start(socket =>
    {
        socket.OnOpen = async () =>
        {
            Console.WriteLine("Open!");
            ConnectionManager.allSockets.Add(socket);
            var ticketService = app.Services.GetRequiredService<TicketService>();
            var currentPriceUpdate = ticketService.SendPriceUpdate();
            await socket.Send(currentPriceUpdate);
        };

        socket.OnClose = () =>
        {
            Console.WriteLine("Close!");
            ConnectionManager.allSockets.Remove(socket);
        };

        socket.OnMessage = async message =>
        {
            await app.InvokeClientEventHandler(clientEventHandlers, socket, message);
            Console.WriteLine(message);
        };
    });

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();

    app.Run();
    


