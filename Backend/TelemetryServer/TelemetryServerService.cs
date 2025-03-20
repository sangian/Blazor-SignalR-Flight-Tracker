using Backend.LiveTelemetry;
using Microsoft.AspNetCore.SignalR;
using Shared;

namespace Backend.TelemetryServer;

public sealed class TelemetryServerService(
    ILogger<TelemetryServerService> logger,
    IHubContext<TelemetryServerHub> telemetryServerHubContext,
    LiveTelemetryService liveTelemetryService,
    TelemetryServerUserManager telemetryServerUserManager)
{
    public async Task StartRequest(int airplaneId)
    {
        logger.LogInformation($"TelemetryService => Starting Airplane {airplaneId}...");

        var userId = $"Airplane-{airplaneId}";

        if (telemetryServerUserManager.IsUserConnected(userId))
        {
            await telemetryServerHubContext.Clients
                .User(userId)
                .SendAsync("StartRequest");
        }
        else
        {
            logger.LogInformation($"TelemetryService => Airplane {airplaneId} is offline.");

            await liveTelemetryService.NotifyAirplaneStartResponse(new GenericResponse
            {
                AirplaneId = airplaneId,
                Success = false,
                ErrorMessage = $"Airplane {airplaneId} is offline."
            });
        }
    }

    public async Task StopRequest(int airplaneId)
    {
        logger.LogInformation($"TelemetryService => Stopping Airplane {airplaneId}...");

        var userId = $"Airplane-{airplaneId}";

        if (telemetryServerUserManager.IsUserConnected(userId))
        {
            await telemetryServerHubContext.Clients
                .User(userId)
                .SendAsync("StopRequest");
        }
        else
        {
            logger.LogInformation($"TelemetryService => Airplane {airplaneId} is offline.");

            await liveTelemetryService.NotifyAirplaneStopResponse(new GenericResponse
            {
                AirplaneId = airplaneId,
                Success = false,
                ErrorMessage = $"Airplane {airplaneId} is offline."
            });
        }
    }

    public async Task AddToGroup(string userId, string groupName)
    {
        var connectionIds = telemetryServerUserManager.GetUserConnections(userId);
        foreach (var connectionId in connectionIds)
        {
            await telemetryServerHubContext.Groups.AddToGroupAsync(connectionId, groupName);
        }
    }

    public async Task RemoveFromGroup(string userId, string groupName)
    {
        var connectionIds = telemetryServerUserManager.GetUserConnections(userId);
        foreach (var connectionId in connectionIds)
        {
            await telemetryServerHubContext.Groups.RemoveFromGroupAsync(connectionId, groupName);
        }
    }

    public async Task PingConnection(string connectionId)
    {
        await telemetryServerHubContext.Clients
            .Client(connectionId)
            .SendAsync("Ping");
    }

    public async Task PingUser(string userId)
    {
        await telemetryServerHubContext.Clients
            .User(userId)
            .SendAsync("Ping");
    }

    public async Task PingGroup(string groupName)
    {
        await telemetryServerHubContext.Clients
            .Group(groupName)
            .SendAsync("Ping");
    }

    public async Task PingAll()
    {
        await telemetryServerHubContext.Clients
            .All
            .SendAsync("Ping");
    }
}
