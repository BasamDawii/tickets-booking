namespace Common.Models
{
    public class TicketData
    {
        public Guid TicketId { get; set; }
        public string EventName { get; set; }
        public string UserName { get; set; }
        public DateTime BookingDate { get; set; }
        public int SeatNumber { get; set; }
    }
}