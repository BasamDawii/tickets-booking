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


    public void RemoveUser(string userName)
    {
        if (_users.ContainsKey(userName))
        {
            _users.Remove(userName);
        }
    }


    public User GetUser(string userName)
    {
        return _users.ContainsKey(userName) ? _users[userName] : null;
    }


    public void BroadcastMessage(string message)
    {
        foreach (var user in _users.Values)
        {
            user.Connection.Send(message);
        }
    }
}