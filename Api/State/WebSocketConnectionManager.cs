using Core.Models;


namespace Api.State;


public class WebSocketConnectionManager : IWebSocketConnectionManager
{
    private readonly Dictionary<string, User> _users = new Dictionary<string, User>();


    public void AddUser(User user)
    {
        if (!_users.ContainsKey(user.Name))
        {
            _users[user.Name] = user;
        }
    }
    
    public void BroadcastMessage(string message)
    {
        foreach (var user in _users.Values)
        {
            user.Connection.Send(message);
        }
    }
}