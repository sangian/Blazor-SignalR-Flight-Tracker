using System.Collections.Concurrent;
using Airplane = TelemetrySimulator.Airplane;

ConcurrentDictionary<int, Airplane> activeAirplanes = new();
CancellationTokenSource cts = new();

try
{
    foreach (var airplaneModel in Shared.Airplane.Airplanes)
    {
        Console.WriteLine($"Preparing Airplane {airplaneModel.Id}...");

        try
        {
            var simulatedAirplane = new Airplane(airplaneModel.Id);

            await simulatedAirplane.InitializeSignalRConnection();

            activeAirplanes.TryAdd(airplaneModel.Id, simulatedAirplane);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ERROR preparing Airplane {airplaneModel.Id}:\n{ex.Message}");
        }
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine("Error preparing airplanes:\n" + ex.Message);
}

// Keep the application running
Console.CancelKeyPress += (sender, e) =>
{
    Console.WriteLine("Application is shutting down...");

    foreach (var airplane in activeAirplanes.Values)
    {
        Console.WriteLine($"Disposing connection for Airplane {airplane.GetAirplaneId()}...");

        airplane.Dispose();
    }

    activeAirplanes.Clear();

    // Signal the cancellation token to stop the Task.Delay
    cts.Cancel();
};

// Keep the application running
while (!cts.IsCancellationRequested)
{
    await Task.Delay(1000);
}

Console.WriteLine("Application has exited gracefully.");
