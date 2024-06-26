using Fleck;

namespace Api.ConnectionsState
{
    public class ConnectionManager
    {
        private readonly Dictionary<Guid, WebSocketWithMetaData> _connections = new();
        private readonly ILogger<ConnectionManager> _logger;
        private readonly ILoggerFactory _loggerFactory;

        public ConnectionManager(ILogger<ConnectionManager> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        public void AddConnection(Guid id, IWebSocketConnection socket)
        {
            var logger = _loggerFactory.CreateLogger<WebSocketWithMetaData>();

            if (!_connections.ContainsKey(id))
            {
                _connections[id] = new WebSocketWithMetaData(socket, logger);
                _logger.LogInformation($"New connection added with GUID: {id}");
            }
            else
            {
                _logger.LogInformation($"Connection with GUID: {id} already exists. Updating socket reference.");
                _connections[id].Connection = socket;
            }
        }

        public void RemoveConnection(Guid id)
        {
            if (_connections.ContainsKey(id))
            {
                _connections[id].Connection.Close();
                _connections.Remove(id);
                _logger.LogInformation($"Connection and associated metadata removed: {id}");
            }
            else
            {
                _logger.LogWarning($"Attempted to remove non-existent connection with GUID: {id}");
            }
        }

        public WebSocketWithMetaData GetConnection(Guid id) => _connections.TryGetValue(id, out var metaData) ? metaData : null;

        public IEnumerable<WebSocketWithMetaData> GetAllConnections() => _connections.Values;

        public bool IsAuthenticated(IWebSocketConnection socket) => _connections.TryGetValue(socket.ConnectionInfo.Id, out WebSocketWithMetaData metaData) && !string.IsNullOrEmpty(metaData.Username);
    }

    public class WebSocketWithMetaData
    {
        private readonly ILogger<WebSocketWithMetaData> _logger;

        public IWebSocketConnection Connection { get; set; }
        public string Username { get; set; }

        public WebSocketWithMetaData(IWebSocketConnection connection, ILogger<WebSocketWithMetaData> logger)
        {
            Connection = connection;
            Username = string.Empty;
            _logger = logger;
        }
    }
}
