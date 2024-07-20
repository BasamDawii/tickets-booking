using Fleck;

namespace tickets_booking;

public static class ConnectionManager
{
    public static List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
}