namespace Core.Models;


public class Ticket
{
    public int TotalTickets { get; set; }
    public int TicketsSold { get; set; }
    public double BasePrice { get; set; }
    public double PriceIncreasePerTicket { get; set; }


    public double CalculateCurrentPrice()
    {
        return BasePrice + (TicketsSold * PriceIncreasePerTicket);
    }
}