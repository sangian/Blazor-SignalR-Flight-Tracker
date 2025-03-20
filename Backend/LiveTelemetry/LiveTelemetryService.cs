using Microsoft.AspNetCore.SignalR;
using Shared;

namespace Backend.LiveTelemetry;

public sealed class LiveTelemetryService(
    ILogger<LiveTelemetryService> logger,
    IHubContext<LiveTelemetryHub> liveTelemetryHubContext)
{
    public async Task NotifyAirplaneStartResponse(GenericResponse genericResponse)
    {
        await liveTelemetryHubContext.Clients.All
            .SendAsync("NotifyAirplaneStartResponse", genericResponse, default);
    }

    public async Task NotifyAirplaneStopResponse(GenericResponse genericResponse)
    {
        await liveTelemetryHubContext.Clients.All
            .SendAsync("NotifyAirplaneStopResponse", genericResponse, default);
    }

    public async Task ReceiveTelemetry(AirplaneTelemetry telemetry)
    {
        logger.LogInformation("LiveTelemetryService => Receiving telemetry...");

        await liveTelemetryHubContext.Clients.All
            .SendAsync("ReceiveTelemetry", telemetry, default);
    }

    public async Task StreamTelemetry(IAsyncEnumerable<AirplaneTelemetry> telemetryStream, CancellationToken cancellationToken)
    {
        await foreach (var telemetry in telemetryStream.WithCancellation(cancellationToken))
        {
            logger.LogInformation("LiveTelemetryService => Receiving telemetry...");

            await liveTelemetryHubContext.Clients.All
            .SendAsync("ReceiveTelemetry", telemetry, default);
        }
    }
}
