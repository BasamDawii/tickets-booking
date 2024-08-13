using Core.Interfaces;
using Core.Models;


namespace Service;


public class TicketService : ITicketService
{
    private readonly Ticket _ticket;
    private readonly Dictionary<string, int> _userTickets;


    public TicketService(Ticket ticket)
    {
        _ticket = ticket;
        _userTickets = new Dictionary<string, int>();
    }


    public bool BuyTicket(string userName)
    {
        if (_ticket.TicketsSold < _ticket.TotalTickets)
        {
            _ticket.TicketsSold++;
            if (_userTickets.ContainsKey(userName))
            {
                _userTickets[userName]++;
            }
            else
            {
                _userTickets[userName] = 1;
            }
            return true;
        }
        return false;
    }


    public bool RefundTicket(string userName)
    {
        if (_userTickets.ContainsKey(userName) && _userTickets[userName] > 0)
        {
            _ticket.TicketsSold--;
            _userTickets[userName]--;
            return true;
        }
        return false;
    }


    public double GetCurrentPrice()
    {
        return _ticket.CalculateCurrentPrice();
    }
}