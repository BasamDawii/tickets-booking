using System.Text.Json;
using lib;
using tickets_booking.Dtos;
using Websocket.Client;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Startup.Statup(null);
    }

    [Test]
    public async Task Test1()
    {
        var ws = await new WebSocketTestClient().ConnectAsync();
        ws.DoAndAssert(new ClientWantsToBuyTicketsDto()
        {
            Topic = "Concert"
        });
            JsonSerializer.Serialize(new ServerSendsInfoToClient
        {
            Message = "Ticket purchase processed successfully"
        });
        
    }
}