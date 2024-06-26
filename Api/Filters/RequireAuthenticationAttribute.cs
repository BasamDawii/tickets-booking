using System.Text.Json;
using Api.ConnectionsState;
using Api.Dtos;
using Common;
using Fleck;
using lib;
using Api.ConnectionsState;

namespace Api.Filters
{
    public class RequireAuthenticationAttribute : BaseEventFilter
    {
        public override Task Handle<T>(IWebSocketConnection socket, T dto)
        {
            var connectionManager = ServiceLocator.ServiceProvider.GetService<ConnectionManager>();
            if (connectionManager == null || !connectionManager.IsAuthenticated(socket))
            {
                socket.Send(JsonSerializer.Serialize(new ServerSendsErrorMessageDto
                {
                    ErrorMessage = "You must sign in before you connect."
                }));
                throw new AppException("Client must be authenticated to use this feature.");
            }

            return Task.CompletedTask;
        }
    }
}

public static class ServiceLocator
{
    public static IServiceProvider ServiceProvider { get; set; }
}