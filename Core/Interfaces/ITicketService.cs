namespace Core.Interfaces;


public interface ITicketService
{
    bool BuyTicket(string userName);
    bool RefundTicket(string userName);
    double GetCurrentPrice();
}