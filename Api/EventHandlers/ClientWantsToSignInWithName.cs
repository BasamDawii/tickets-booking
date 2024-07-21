using Api.Dtos;
using Api.State;
using Core.Interfaces;
using Core.Models;
using Fleck;
using lib;


namespace Api.EventHandlers;


public class ClientWantsToSignInWithName : BaseEventHandler<ClientWantsToSignInWithNameDto>
{
    private readonly IWebSocketConnectionManager _connectionManager;
    private readonly ITicketService _ticketService;
    public ClientWantsToSignInWithName(ITicketService ticketService, IWebSocketConnectionManager connectionManager)
    {
        _ticketService = ticketService;
        _connectionManager = connectionManager;
    }


    public override Task Handle(ClientWantsToSignInWithNameDto dto, IWebSocketConnection socket)
    {
        var user = new User
        {
            Name = dto.Name,
            Connection = socket
        };
        _connectionManager.AddUser(user);
        var currentPrice = _ticketService.GetCurrentPrice();
        socket.Send($"Current Price: {currentPrice:C}");
        return Task.CompletedTask;
    }
}