using Fleck;


namespace Core.Models;


public class User
{
    public string Name { get; set; }
    public IWebSocketConnection Connection { get; set; }
}