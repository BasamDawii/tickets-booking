using System.Text.Json;
using tickets_booking.Dtos;
using Fleck;
using lib;

namespace tickets_booking.EventHandlers
{
    public class ClientWantsToBuyTicketsHandler : BaseEventHandler<ClientWantsToBuyTicketsDto>
    {
        private readonly ILogger<ClientWantsToBuyTicketsHandler> _logger;

        public ClientWantsToBuyTicketsHandler(ILogger<ClientWantsToBuyTicketsHandler> logger)
        {
            _logger = logger;
        }

        public override async Task Handle(ClientWantsToBuyTicketsDto dto, IWebSocketConnection socket)
        {
            try
            {
                
                Console.WriteLine($"Handling buy tickets for topic: {dto.Topic}");
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
