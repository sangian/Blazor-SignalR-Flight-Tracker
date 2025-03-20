using Backend.LiveTelemetry;
using Microsoft.AspNetCore.SignalR;
using Shared;
using System.Text.Json;
using System.Threading.Channels;

namespace Backend.TelemetryServer;

public sealed class TelemetryServerHub(
    ILogger<TelemetryServerHub> logger,
    TelemetryServerUserManager telemetryServerUserManager,
    LiveTelemetryService liveTelemetryService,
    StreamChannelManager streamChannelManager) : Hub
{
    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        var connectionId = Context.ConnectionId;

        if (string.IsNullOrEmpty(userId))
        {
            logger.LogWarning("TelemetryServerHub => Client connected with no user ID: {connectionId}", connectionId);
        }
        else
        {
            logger.LogInformation("TelemetryServerHub => Client connected: {userId} {connectionId}", userId, connectionId);
            telemetryServerUserManager.AddUser(userId, connectionId);
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.Identity?.Name;
        var connectionId = Context.ConnectionId;

        if (string.IsNullOrEmpty(userId))
        {
            logger.LogWarning("TelemetryServerHub => Client disconnected with no user ID: {connectionId}", connectionId);
        }
        else
        {
            logger.LogInformation("TelemetryServerHub => Client disconnected: {userId} {connectionId}", userId, connectionId);
            telemetryServerUserManager.RemoveUser(userId, connectionId);
        }

        if (exception is not null)
        {
            logger.LogError(exception, "TelemetryServerHub => Client {connectionId} disconnected with error: {Error}", connectionId, exception.Message);
        }

        return base.OnDisconnectedAsync(exception);
    }

    public void Pong(int airplaneId)
    {
        logger.LogInformation("Received PONG from Airplane {airplaneId}", airplaneId);
    }

    public async Task StartResponse(GenericResponse response)
    {
        await liveTelemetryService.NotifyAirplaneStartResponse(response);
    }

    public async Task StopResponse(GenericResponse response)
    {
        await liveTelemetryService.NotifyAirplaneStopResponse(response);
    }

    public async Task ReceiveTelemetry(AirplaneTelemetry telemetry)
    {
        logger.LogInformation("TelemetryServerHub => Telemetry received from client: {Telemetry}", JsonSerializer.Serialize(telemetry));

        await liveTelemetryService.ReceiveTelemetry(telemetry);
    }

    public async Task StreamTelemetry(ChannelReader<AirplaneTelemetry> stream)
    {
        while (await stream.WaitToReadAsync())
        {
            while (stream.TryRead(out AirplaneTelemetry? telemetry))
            {
                logger.LogInformation("TelemetryServerHub => Telemetry stream received from client: {Telemetry}", JsonSerializer.Serialize(telemetry));

                streamChannelManager.StreamTelemetry(telemetry);
            }
        }
    }
}
