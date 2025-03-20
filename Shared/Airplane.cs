namespace Shared;

public record Airplane
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Model { get; init; }
    public string? Manufacturer { get; init; }
    public string? Color { get; init; }

    public static readonly IReadOnlyCollection<Airplane> Airplanes =
    [
        new() { Id = 1, Name = "PK-ABC", Model = "787", Manufacturer = "Boeing", Color = "#FF0000" }, // Bright Red
        new() { Id = 2, Name = "PK-DEF", Model = "777", Manufacturer = "Boeing", Color = "#00FF00" }, // Bright Green
        new() { Id = 3, Name = "PK-GHI", Model = "737", Manufacturer = "Boeing", Color = "#0000FF" }, // Bright Blue
        new() { Id = 4, Name = "PK-JKL", Model = "A350", Manufacturer = "Airbus", Color = "#FFA500" }, // Bright Orange
        new() { Id = 5, Name = "PK-MNO", Model = "A320", Manufacturer = "Airbus", Color = "#00FFFF" }, // Bright Cyan
        new() { Id = 6, Name = "PK-PQR", Model = "A330neo", Manufacturer = "Airbus", Color = "#FF00FF" }  // Bright Pink
    ];

}
