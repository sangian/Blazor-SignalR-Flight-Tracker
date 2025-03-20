using GoogleMapsComponents.Maps;
using Shared;

namespace Frontend.Models;

public class MapMarker
{
    public int Id { get; set; }
    public required City Source { get; set; }
    public required City Destination { get; set; }
    public required LatLngLiteral Current { get; set; }
    public double Heading { get; set; } // In degrees (0-360)
    public double Altitude { get; set; } // In meters
    public double Velocity { get; set; } // In km/h
    public double Eta { get; set; } // In minutes
    public string? Status { get; set; }
    public string? Label { get; set; }
    public bool Clickable { get; set; } = true;
    public bool Draggable { get; set; }
    public bool Active { get; set; }
}
