using Microsoft.AspNetCore.SignalR.Client;
using Shared;
using System.Threading.Channels;

namespace TelemetrySimulator;

public sealed partial class Airplane(int airplaneId) : IDisposable
{
    private const string HubUrl = "http://localhost:5039/hub/telemetry-server";
    private HubConnection? connection;
    private Channel<AirplaneTelemetry>? streamChannel;

    private const double MinimumSimulationDistanceKm = 11000.0;
    private const int MaxSimulationDurationSeconds = 300;

    private CancellationTokenSource? cancellationTokenSource;
    private Task? SimulationTask;

    private static readonly Random random = new();
    private bool disposed = false;

    public int GetAirplaneId()
    {
        return airplaneId;
    }

    public async Task InitializeSignalRConnection()
    {
        var userId = $"Airplane-{airplaneId}";
        var clientAuthToken = TokenGenerator.GetJwtToken(userId, "Airplane");

        connection = new HubConnectionBuilder()
            .WithUrl(HubUrl, options =>
            {
                options.AccessTokenProvider = async () =>
                {
                    return await Task.FromResult(clientAuthToken);
                };
            })
            .WithAutomaticReconnect()
            .Build();

        connection.Reconnecting += OnReconnecting!;
        connection.Reconnected += OnReconnected!;
        connection.Closed += OnClosedPermanently!;

        HandlePingEvent();
        HandleStartEvent();
        HandleStopEvent();

        await ConnectToSignalRHub();
        StartStreamingChannel();
    }

    private async Task ConnectToSignalRHub()
    {
        try
        {
            Console.WriteLine($"Airplane {airplaneId}: Connecting to SignalR server...");

            await connection!.StartAsync();

            Console.WriteLine($"Airplane {airplaneId}: Connected to SignalR server! Connection ID: {connection.ConnectionId}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Airplane {airplaneId}: ERROR: Cannot connect to SignalR server:\n{ex.Message}");
            return;
        }
    }

    private void StartStreamingChannel()
    {
        StopStreamingChannel();

        streamChannel = Channel.CreateUnbounded<AirplaneTelemetry>();
        _ = connection!.SendAsync("StreamTelemetry", streamChannel.Reader);
    }

    private void StopStreamingChannel()
    {
        if (streamChannel is not null)
        {
            streamChannel.Writer.Complete();
            streamChannel = null;
        }
    }

    private Task OnReconnecting(Exception error)
    {
        Console.WriteLine($"Airplane {airplaneId}: Re-connecting to SignalR server...");

        if (error is not null)
        {
            Console.Error.WriteLine($"Airplane {airplaneId}: ERROR: {error}");
        }

        return Task.CompletedTask;
    }

    private Task OnReconnected(string connectionId)
    {
        Console.WriteLine($"Airplane {airplaneId}: Re-connected to SignalR server! Connection ID: {connectionId}");
        
        StartStreamingChannel();

        return Task.CompletedTask;
    }

    private async Task OnClosedPermanently(Exception error)
    {
        Console.WriteLine($"Airplane {airplaneId}: Connection closed permanently");

        if (error is not null)
        {
            Console.WriteLine(error);
        }

        await Task.Delay(5000); // Wait before attempting to reconnect

        await ConnectToSignalRHub();
    }

    private void HandlePingEvent()
    {
        connection!.On("Ping", async () => {
            Console.WriteLine($"Airplane {airplaneId} => Received PING request");
            await connection!.SendAsync("Pong", airplaneId); // Send PONG response
        });
    }

    private void HandleStartEvent()
    {
        Console.WriteLine();
        connection!.On("StartRequest", async () =>
        {
            try
            {
                Console.WriteLine($"Airplane {airplaneId}: Server invoked 'Start'. Starting simulation...");

                if (IsSimulationRunning())
                {
                    Console.WriteLine($"Airplane {airplaneId}: Airplane already started.");

                    await SendEventResponse("StartResponse", new GenericResponse
                    {
                        AirplaneId = airplaneId,
                        Success = false,
                        ErrorMessage = $"Airplane {airplaneId} already started.",
                    });

                    return;
                }

                StartSimulation();

                Console.WriteLine($"Airplane {airplaneId}: Airplane started.");

                await SendEventResponse("StartResponse", new GenericResponse
                {
                    AirplaneId = airplaneId,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Airplane {airplaneId}: Error starting airplane.\n{ex.Message}");

                await SendEventResponse("StartResponse", new GenericResponse
                {
                    AirplaneId = airplaneId,
                    Success = false,
                    ErrorMessage = ex.Message,
                });
            }
        });
    }

    private void HandleStopEvent()
    {
        connection!.On("StopRequest", async () =>
        {
            try
            {
                Console.WriteLine($"Airplane {airplaneId}: Server invoked 'Stop'. Stopping simulation...");

                if (!IsSimulationRunning())
                {
                    Console.WriteLine($"Airplane {airplaneId}: Airplane already stopped.");

                    await SendEventResponse("StopResponse", new GenericResponse
                    {
                        AirplaneId = airplaneId,
                        Success = false,
                        ErrorMessage = $"Airplane {airplaneId} is already stopped.",
                    });

                    return;
                }

                StopSimulation();

                Console.WriteLine($"Airplane {airplaneId}: Airplane stopped.");

                await SendEventResponse("StopResponse", new GenericResponse
                {
                    AirplaneId = airplaneId,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Airplane {airplaneId}: Error stopping airplane.\n{ex.Message}");

                await SendEventResponse("StopResponse", new GenericResponse
                {
                    AirplaneId = airplaneId,
                    Success = false,
                    ErrorMessage = ex.Message,
                });
            }
        });
    }

    private bool IsSimulationRunning()
    {
        return SimulationTask is not null && !SimulationTask.IsCompleted;
    }

    private void StartSimulation()
    {
        cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        SimulationTask = Task.Run(async () =>
        {
            try
            {
                var majorCities = City.GetMajorCities();

                var (sourceCity, destinationCity) = SelectSourceAndDestination(majorCities);

                double sourceLat = sourceCity.Latitude;
                double sourceLon = sourceCity.Longitude;

                double destLat = destinationCity.Latitude;
                double destLon = destinationCity.Longitude;

                string status = "Taxiing";
                double curLat = sourceLat, curLon = sourceLon;
                double altitude = 0, velocity = 0, etaMinutes = 0;

                double totalDistanceKm = CalculateGreatCircleDistance(sourceLat, sourceLon, destLat, destLon);

                // Distance to move each second
                double distancePerStep = totalDistanceKm / MaxSimulationDurationSeconds;

                // Simulate the flight step-by-step
                for (int i = 0; i <= MaxSimulationDurationSeconds; i++)
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    // Calculate progress as a fraction of the total duration (0.0 to 1.0)
                    double progress = (double)i / MaxSimulationDurationSeconds;

                    // Recalculate the bearing from the current position toward the destination.
                    double currentHeading = CalculateHeading(curLat, curLon, destLat, destLon);

                    // Update the current position by moving distancePerStep along the current heading.
                    (curLat, curLon) = CalculateDestinationPoint(curLat, curLon, distancePerStep, currentHeading);

                    // Calculate remaining distance to the destination
                    double remainingDistanceKm = CalculateGreatCircleDistance(curLat, curLon, destLat, destLon);

                    // Determine altitude, velocity, ETA and status based on progress
                    (altitude, velocity, etaMinutes, status) = CalculateTelemetry(progress, remainingDistanceKm);

                    // Prepare telemetry data.
                    AirplaneTelemetry telemetry = new()
                    {
                        AirplaneId = airplaneId,
                        Source = sourceCity,
                        Destination = destinationCity,
                        Current = new LocationTelemetry { Latitude = curLat, Longitude = curLon },
                        Altitude = altitude,
                        Velocity = velocity,
                        Heading = currentHeading,
                        Eta = etaMinutes,
                        Status = status
                    };

                    Console.WriteLine($"Airplane {airplaneId}: Sending telemetry - " +
                                      $"Alt={altitude:F1}m, Vel={velocity:F1}km/h, Head={currentHeading:F1}°, Status={status}");

                    await StreamTelemetry(telemetry);

                    await Task.Delay(1000, cancellationToken); // 1-second interval
                }

                await SendEventResponse("AirplaneArrived", new GenericResponse
                {
                    AirplaneId = airplaneId,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Airplane {airplaneId}: Simulation ERROR: {ex.Message}");

                await SendEventResponse("AirplaneCrashed", new GenericResponse
                {
                    AirplaneId = airplaneId,
                    Success = true,
                    ErrorMessage = ex.Message
                });
            }
        }, cancellationToken);
    }

    private void StopSimulation()
    {
        if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
        {
            cancellationTokenSource.Cancel();
        }
        else
        {
            Console.WriteLine($"Airplane {airplaneId}: Simulation is not running.");
        }
    }

    private async Task SendEventResponse(string serverMethodName, GenericResponse response)
    {
        await connection!.SendAsync(serverMethodName, response);
    }

    private async Task StreamTelemetry(AirplaneTelemetry telemetry)
    {
        if (streamChannel is not null)
        {
            await streamChannel.Writer.WriteAsync(telemetry);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                cancellationTokenSource?.Cancel();
                connection?.StopAsync().Wait();
                connection?.DisposeAsync().AsTask().Wait();
            }

            // Dispose unmanaged resources

            disposed = true;
        }
    }

    ~Airplane()
    {
        Dispose(false);
    }
}



