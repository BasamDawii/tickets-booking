namespace tickets_booking.Dtos;

using lib;

 
public class ServerSendsErrorMessageDto : BaseDto
{
    public string ErrorMessage { get; set; }
}

public class ClientWantsToBuyTicketsDto : BaseDto
{
    public string BuyComand { get; set; }
}
 

public class ServerSendsInfoToClient : BaseDto
{
    public string Message { get; set; }
}

