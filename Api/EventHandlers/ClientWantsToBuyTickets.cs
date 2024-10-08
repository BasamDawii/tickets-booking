using Api.Dtos;
using Api.State;
using Core.Interfaces;
using Fleck;
using lib;


namespace Api.EventHandlers;


public class ClientWantsToBuyTicket : BaseEventHandler<ClientWantsToBuyTicketDto>
{
    private readonly ITicketService _ticketService;
    private readonly IWebSocketConnectionManager _connectionManager;


    public ClientWantsToBuyTicket(ITicketService ticketService, IWebSocketConnectionManager connectionManager)
    {
        _ticketService = ticketService;
        _connectionManager = connectionManager;
    }


    public override Task Handle(ClientWantsToBuyTicketDto dto, IWebSocketConnection socket)
    {
        if (_ticketService.BuyTicket(dto.UserName))
        {
            var currentPrice = _ticketService.GetCurrentPrice();
            _connectionManager.BroadcastMessage($"Current Price: {currentPrice:C}");
        }
        else
        {
            socket.Send("No more tickets available.");
        }
        return Task.CompletedTask;
    }
}