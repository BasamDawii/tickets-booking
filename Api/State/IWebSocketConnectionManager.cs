using Core.Models;


namespace Api.State;


public interface IWebSocketConnectionManager
{
    void AddUser(User user);
    void BroadcastMessage(string message);
}