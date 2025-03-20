using City = Shared.City;

namespace TelemetrySimulator;

public sealed partial class Airplane
{
    private static (double altitude, double velocity, double etaMinutes, string status) CalculateTelemetry(double progress, double remainingDistanceKm)
    {
        double altitude, velocity, etaMinutes;
        string status;

        if (progress <= 0.05)
        {
            altitude = progress * (50 / 0.05); // 0m to 50m
            velocity = progress * (300 / 0.05); // 0km/h to 300km/h
            etaMinutes = CalculateEtaMinutes(remainingDistanceKm, velocity);
            status = "Taking off";
        }
        else if (progress <= 0.2)
        {
            altitude = 50 + (progress - 0.05) * (10000 - 50) / (0.2 - 0.05); // 50m to 10000m
            velocity = 300 + (progress - 0.05) * (900 - 300) / (0.2 - 0.05); // 300km/h to 900km/h
            etaMinutes = CalculateEtaMinutes(remainingDistanceKm, velocity);
            status = "Enroute";
        }
        else if (progress <= 0.8)
        {
            altitude = 10000; // Maintain 10000m
            velocity = 900; // Maintain 900km/h
            etaMinutes = CalculateEtaMinutes(remainingDistanceKm, velocity);
            status = "Enroute";
        }
        else if (progress <= 0.95)
        {
            altitude = 10000 - (progress - 0.8) * (10000 - 200) / (0.95 - 0.8); // 10000m to 200m
            velocity = 900 - (progress - 0.8) * (900 - 300) / (0.95 - 0.8); // 900km/h to 300km/h
            etaMinutes = CalculateEtaMinutes(remainingDistanceKm, velocity);
            status = "Enroute";
        }
        else
        {
            altitude = 200 - (progress - 0.95) * (200 - 0) / (1.0 - 0.95); // 200m to 0m
            velocity = 300 - (progress - 0.95) * (300 - 0) / (1.0 - 0.95); // 300km/h to 0km/h
            etaMinutes = 0;
            status = "Landing";
        }

        if (progress == 1.0)
        {
            altitude = 0;
            velocity = 0;
            etaMinutes = 0;
            status = "Arrived";
        }

        return (altitude, velocity, etaMinutes, status);
    }

    private static (City sourceCity, City destinationCity) SelectSourceAndDestination(IReadOnlyCollection<City> cities)
    {
        var cityList = cities.ToList();
        City sourceCity = cityList[random.Next(cityList.Count)];
        City destinationCity = cityList[random.Next(cityList.Count)];

        // Ensure source and destination are distinct and satisfy the minimum distance.
        while (destinationCity == sourceCity ||
               CalculateGreatCircleDistance(sourceCity.Latitude, sourceCity.Longitude, destinationCity.Latitude, destinationCity.Longitude) < MinimumSimulationDistanceKm)
        {
            destinationCity = cityList[random.Next(cityList.Count)];
        }

        return (sourceCity, destinationCity);
    }

    private static double CalculateGreatCircleDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Convert degrees to radians
        double dLat = (lat2 - lat1) * Math.PI / 180.0;
        double dLon = (lon2 - lon1) * Math.PI / 180.0;
        lat1 = lat1 * Math.PI / 180.0;
        lat2 = lat2 * Math.PI / 180.0;

        // Haversine formula
        double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dLon / 2), 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        const double EarthRadiusKm = 6371.0;
        return EarthRadiusKm * c;
    }

    /// <summary>
    /// Calculates the initial bearing (forward azimuth) from point A to point B along a great circle.
    /// </summary>
    private static double CalculateHeading(double lat1, double lon1, double lat2, double lon2)
    {
        // Convert degrees to radians
        double lat1Rad = lat1 * Math.PI / 180.0;
        double lon1Rad = lon1 * Math.PI / 180.0;
        double lat2Rad = lat2 * Math.PI / 180.0;
        double lon2Rad = lon2 * Math.PI / 180.0;

        double deltaLon = lon2Rad - lon1Rad;
        double y = Math.Sin(deltaLon) * Math.Cos(lat2Rad);
        double x = Math.Cos(lat1Rad) * Math.Sin(lat2Rad) -
                   Math.Sin(lat1Rad) * Math.Cos(lat2Rad) * Math.Cos(deltaLon);

        double headingRad = Math.Atan2(y, x);
        double headingDeg = headingRad * 180.0 / Math.PI;
        return (headingDeg + 360.0) % 360.0; // Normalize to range [0,360)
    }

    /// <summary>
    /// Computes the destination point given a start point, bearing, and distance along a great circle.
    /// </summary>
    private static (double lat, double lon) CalculateDestinationPoint(double lat, double lon, double distanceKm, double headingDegrees)
    {
        const double R = 6371.0; // Earth's radius in km
        double headingRad = headingDegrees * Math.PI / 180.0;
        double latRad = lat * Math.PI / 180.0;
        double lonRad = lon * Math.PI / 180.0;
        double angularDistance = distanceKm / R;

        double newLatRad = Math.Asin(
            Math.Sin(latRad) * Math.Cos(angularDistance) +
            Math.Cos(latRad) * Math.Sin(angularDistance) * Math.Cos(headingRad)
        );

        double newLonRad = lonRad + Math.Atan2(
            Math.Sin(headingRad) * Math.Sin(angularDistance) * Math.Cos(latRad),
            Math.Cos(angularDistance) - Math.Sin(latRad) * Math.Sin(newLatRad)
        );

        double newLat = newLatRad * 180.0 / Math.PI;
        double newLon = newLonRad * 180.0 / Math.PI;
        return (newLat, newLon);
    }

    private static double CalculateEtaMinutes(double remainingDistanceKm, double velocityKmPerHour)
    {
        double etaMinutes = 0;
        double velocityKmPerSec = velocityKmPerHour / 3600.0;

        if (velocityKmPerSec > 0)
        {
            etaMinutes = (remainingDistanceKm / (velocityKmPerSec * 3600.0)) * 60.0;
        }

        return etaMinutes;
    }
}
