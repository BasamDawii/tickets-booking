using lib;


namespace Api.Dtos;


public class ClientWantsToSignInWithNameDto : BaseDto
{
    public string Name { get; set; }
}


public class ClientWantsToBuyTicketDto : BaseDto
{
    public string UserName { get; set; }
}


public class ClientWantsToRefundTicketDto : BaseDto
{
    public string UserName { get; set; }
}