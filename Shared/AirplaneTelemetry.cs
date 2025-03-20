namespace Shared;

public record AirplaneTelemetry
{
    public int AirplaneId { get; init; }
    public required City Source { get; init; }
    public required LocationTelemetry Current { get; init; }
    public required City Destination { get; init; }
    public double Altitude { get; init; } // In meters
    public double Velocity { get; init; } // In km/h
    public double Heading { get; init; } // In degrees (0-360)
    public double Eta { get; init; } // In minutes
    public required string Status { get; init; }
}

public record LocationTelemetry
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
