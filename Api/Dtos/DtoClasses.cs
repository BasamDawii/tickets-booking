using System.ComponentModel.DataAnnotations;
using Common.Models;
using lib;

namespace Api.Dtos
{
    public class ClientWantsToBookTicketDto : BaseDto
    {
        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; }
        
        [Required(ErrorMessage = "Seat number is required")]
        public int SeatNumber { get; set; }
    }

    public class ServerSendsBookingConfirmationDto : BaseDto
    {
        public Guid TicketId { get; set; }
        public string Message { get; set; }
    }

    public class ClientWantsToViewTicketsDto : BaseDto
    {
        
    }

    public class ServerSendsTicketsInfoDto : BaseDto
    {
        public List<TicketData> Tickets { get; set; }
    }
}