using Microsoft.AspNetCore.SignalR;
using Shared;
using System.Threading.Channels;

namespace Backend.LiveTelemetry;

public sealed class LiveTelemetryHub(
    ILogger<LiveTelemetryHub> logger,
    StreamChannelManager streamChannelManager) : Hub
{
    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        var connectionId = Context.ConnectionId;

        if (string.IsNullOrEmpty(userId))
        {
            logger.LogWarning("LiveTelemetryHub => Client connected with no user ID: {connectionId}", connectionId);
        }
        else
        {
            logger.LogInformation("LiveTelemetryHub => Client connected: {userId} {connectionId}", userId, connectionId);
        }

        var channel = Channel.CreateUnbounded<AirplaneTelemetry>();
        streamChannelManager.AddChannel(connectionId, channel);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.Identity?.Name;
        var connectionId = Context.ConnectionId;

        if (string.IsNullOrEmpty(userId))
        {
            logger.LogWarning("LiveTelemetryHub => Client disconnected with no user ID: {connectionId}", connectionId);
        }
        else
        {
            logger.LogInformation("LiveTelemetryHub => Client disconnected: {userId} {connectionId}", userId, connectionId);
        }

        if (exception is not null)
        {
            logger.LogError(exception, "LiveTelemetryHub => Client {connectionId} disconnected with error: {Error}", connectionId, exception.Message);
        }

        streamChannelManager.RemoveChannel(connectionId);

        return base.OnDisconnectedAsync(exception);
    }

    public ChannelReader<AirplaneTelemetry>? StreamTelemetry()
    {
        var connectionId = Context.ConnectionId;

        if (!streamChannelManager.TryGetChannel(connectionId, out var channel))
        {
            logger.LogError("LiveTelemetryHub => Failed to get stream channel for connection ID: {connectionId}", connectionId);

            return null;
        }

        return channel?.Reader;
    }
}
