using Api.Dtos;
using Api.State;
using Core.Interfaces;
using Fleck;
using lib;


namespace Api.EventHandlers;


public class ClientWantsToRefundTicket : BaseEventHandler<ClientWantsToRefundTicketDto>
{
    private readonly ITicketService _ticketService;
    private readonly IWebSocketConnectionManager _connectionManager;


    public ClientWantsToRefundTicket(ITicketService ticketService, IWebSocketConnectionManager connectionManager)
    {
        _ticketService = ticketService;
        _connectionManager = connectionManager;
    }


    public override Task Handle(ClientWantsToRefundTicketDto dto, IWebSocketConnection socket)
    {
        if (_ticketService.RefundTicket(dto.UserName))
        {
            var currentPrice = _ticketService.GetCurrentPrice();
            _connectionManager.BroadcastMessage($"Current Price: {currentPrice:C}");
        }
        else
        {
            socket.Send("No tickets to refund.");
        }
        return Task.CompletedTask;
    }
}