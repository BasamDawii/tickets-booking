using System.Text.Json;
using tickets_booking.Dtos;
using Fleck;
using lib;
using Service;

namespace tickets_booking.EventHandlers
{
    public class ClientWantsToBuyTicketsHandler : BaseEventHandler<ClientWantsToBuyTicketsDto>
    {
        private readonly ILogger<ClientWantsToBuyTicketsHandler> _logger;

        private readonly TicketService _ticketService;

        public ClientWantsToBuyTicketsHandler(ILogger<ClientWantsToBuyTicketsHandler> logger, TicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public override async Task Handle(ClientWantsToBuyTicketsDto dto, IWebSocketConnection socket)
        {
            try
            {
                _ticketService.BuyTicket();

                var currentPriceUpdate = _ticketService.SendPriceUpdate();
                foreach (var connection in ConnectionManager.allSockets)
                {
                    if (connection.IsAvailable)
                    {
                        Console.WriteLine($"Sending price update to client: {currentPriceUpdate}");
                        await connection.Send(currentPriceUpdate);
                    }
                }

                await socket.Send(JsonSerializer.Serialize(new ServerSendsInfoToClient
                {
                    Message = "Ticket purchase processed successfully"
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the ticket purchase.");
                await socket.Send(JsonSerializer.Serialize(new ServerSendsErrorMessageDto
                {
                    ErrorMessage = "An unexpected error occurred while processing your request."
                }));
            }
        }
    }
}
