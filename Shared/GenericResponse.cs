namespace Shared;

public record GenericResponse
{
    public int AirplaneId { get; init; }
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
}
