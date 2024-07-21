using Core.Models;


namespace Api.State;


public interface IWebSocketConnectionManager
{
    void AddUser(User user);
    void RemoveUser(string userName);
    User GetUser(string userName);
    void BroadcastMessage(string message);
}