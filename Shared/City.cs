namespace Shared;

public record City
{
    public required string Name { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public required string Country { get; init; }

    public static IReadOnlyCollection<City> GetMajorCities()
    {
        IReadOnlyCollection<City> cities =
        [
            // USA
            new City { Name = "New York", Latitude = 40.7128, Longitude = -74.0060, Country = "USA" },
            new City { Name = "Los Angeles", Latitude = 34.0522, Longitude = -118.2437, Country = "USA" },
            new City { Name = "Chicago", Latitude = 41.8781, Longitude = -87.6298, Country = "USA" },
            new City { Name = "Houston", Latitude = 29.7604, Longitude = -95.3698, Country = "USA" },
            new City { Name = "Phoenix", Latitude = 33.4484, Longitude = -112.0740, Country = "USA" },
            new City { Name = "Philadelphia", Latitude = 39.9526, Longitude = -75.1652, Country = "USA" },
            new City { Name = "San Antonio", Latitude = 29.4241, Longitude = -98.4936, Country = "USA" },
            new City { Name = "San Diego", Latitude = 32.7157, Longitude = -117.1611, Country = "USA" },
            new City { Name = "Dallas", Latitude = 32.7767, Longitude = -96.7970, Country = "USA" },
            new City { Name = "San Jose", Latitude = 37.3382, Longitude = -121.8863, Country = "USA" },

            // United Kingdom
            new City { Name = "London", Latitude = 51.5074, Longitude = -0.1278, Country = "UK" },
            new City { Name = "Birmingham", Latitude = 52.4862, Longitude = -1.8904, Country = "UK" },
            new City { Name = "Manchester", Latitude = 53.4808, Longitude = -2.2426, Country = "UK" },
            new City { Name = "Glasgow", Latitude = 55.8642, Longitude = -4.2518, Country = "UK" },

            // Canada
            new City { Name = "Toronto", Latitude = 43.6532, Longitude = -79.3832, Country = "Canada" },
            new City { Name = "Montreal", Latitude = 45.5017, Longitude = -73.5673, Country = "Canada" },
            new City { Name = "Vancouver", Latitude = 49.2827, Longitude = -123.1207, Country = "Canada" },
            new City { Name = "Calgary", Latitude = 51.0447, Longitude = -114.0719, Country = "Canada" },

            // Mexico
            new City { Name = "Mexico City", Latitude = 19.4326, Longitude = -99.1332, Country = "Mexico" },
            new City { Name = "Guadalajara", Latitude = 20.6597, Longitude = -103.3496, Country = "Mexico" },
            new City { Name = "Monterrey", Latitude = 25.6866, Longitude = -100.3161, Country = "Mexico" },

            // Brazil
            new City { Name = "São Paulo", Latitude = -23.5505, Longitude = -46.6333, Country = "Brazil" },
            new City { Name = "Rio de Janeiro", Latitude = -22.9068, Longitude = -43.1729, Country = "Brazil" },
            new City { Name = "Brasília", Latitude = -15.7939, Longitude = -47.8828, Country = "Brazil" },
            new City { Name = "Salvador", Latitude = -12.9777, Longitude = -38.5016, Country = "Brazil" },

            // Argentina
            new City { Name = "Buenos Aires", Latitude = -34.6037, Longitude = -58.3816, Country = "Argentina" },
            new City { Name = "Córdoba", Latitude = -31.4201, Longitude = -64.1888, Country = "Argentina" },
            new City { Name = "Rosario", Latitude = -32.9442, Longitude = -60.6393, Country = "Argentina" },

            // Colombia
            new City { Name = "Bogotá", Latitude = 4.7110, Longitude = -74.0721, Country = "Colombia" },
            new City { Name = "Medellín", Latitude = 6.2442, Longitude = -75.5812, Country = "Colombia" },
            new City { Name = "Cali", Latitude = 3.4516, Longitude = -76.5320, Country = "Colombia" },

            // Chile
            new City { Name = "Santiago", Latitude = -33.4489, Longitude = -70.6693, Country = "Chile" },
            new City { Name = "Valparaíso", Latitude = -33.0458, Longitude = -71.6197, Country = "Chile" },
            new City { Name = "Concepción", Latitude = -36.8201, Longitude = -73.0444, Country = "Chile" },

            // Peru
            new City { Name = "Lima", Latitude = -12.0464, Longitude = -77.0428, Country = "Peru" },
            new City { Name = "Arequipa", Latitude = -16.4090, Longitude = -71.5375, Country = "Peru" },

            // Cuba
            new City { Name = "Havana", Latitude = 23.1136, Longitude = -82.3666, Country = "Cuba" },
            new City { Name = "Santiago de Cuba", Latitude = 20.0244, Longitude = -75.8219, Country = "Cuba" },

            // France
            new City { Name = "Paris", Latitude = 48.8566, Longitude = 2.3522, Country = "France" },
            new City { Name = "Marseille", Latitude = 43.2965, Longitude = 5.3698, Country = "France" },
            new City { Name = "Lyon", Latitude = 45.7640, Longitude = 4.8357, Country = "France" },
            new City { Name = "Toulouse", Latitude = 43.6047, Longitude = 1.4442, Country = "France" },

            // Germany
            new City { Name = "Berlin", Latitude = 52.5200, Longitude = 13.4050, Country = "Germany" },
            new City { Name = "Munich", Latitude = 48.1351, Longitude = 11.5820, Country = "Germany" },
            new City { Name = "Frankfurt", Latitude = 50.1109, Longitude = 8.6821, Country = "Germany" },
            new City { Name = "Hamburg", Latitude = 53.5511, Longitude = 9.9937, Country = "Germany" },

            // Italy
            new City { Name = "Rome", Latitude = 41.9028, Longitude = 12.4964, Country = "Italy" },
            new City { Name = "Milan", Latitude = 45.4642, Longitude = 9.1900, Country = "Italy" },
            new City { Name = "Naples", Latitude = 40.8518, Longitude = 14.2681, Country = "Italy" },
            new City { Name = "Turin", Latitude = 45.0703, Longitude = 7.6869, Country = "Italy" },

            // Spain
            new City { Name = "Madrid", Latitude = 40.4168, Longitude = -3.7038, Country = "Spain" },
            new City { Name = "Barcelona", Latitude = 41.3851, Longitude = 2.1734, Country = "Spain" },
            new City { Name = "Valencia", Latitude = 39.4699, Longitude = -0.3763, Country = "Spain" },
            new City { Name = "Seville", Latitude = 37.3891, Longitude = -5.9845, Country = "Spain" },

            // Russia
            new City { Name = "Moscow", Latitude = 55.7558, Longitude = 37.6173, Country = "Russia" },
            new City { Name = "St. Petersburg", Latitude = 59.9311, Longitude = 30.3609, Country = "Russia" },
            new City { Name = "Novosibirsk", Latitude = 55.0084, Longitude = 82.9357, Country = "Russia" },

            // Turkey
            new City { Name = "Istanbul", Latitude = 41.0082, Longitude = 28.9784, Country = "Turkey" },
            new City { Name = "Ankara", Latitude = 39.9208, Longitude = 32.8541, Country = "Turkey" },
            new City { Name = "Izmir", Latitude = 38.4237, Longitude = 27.1428, Country = "Turkey" },

            // Egypt
            new City { Name = "Cairo", Latitude = 30.0444, Longitude = 31.2357, Country = "Egypt" },
            new City { Name = "Alexandria", Latitude = 31.2001, Longitude = 29.9187, Country = "Egypt" },
            new City { Name = "Giza", Latitude = 30.0131, Longitude = 31.2089, Country = "Egypt" },

            // South Africa
            new City { Name = "Johannesburg", Latitude = -26.2041, Longitude = 28.0473, Country = "South Africa" },
            new City { Name = "Cape Town", Latitude = -33.9249, Longitude = 18.4241, Country = "South Africa" },
            new City { Name = "Durban", Latitude = -29.8587, Longitude = 31.0218, Country = "South Africa" },

            // Nigeria
            new City { Name = "Lagos", Latitude = 6.5244, Longitude = 3.3792, Country = "Nigeria" },
            new City { Name = "Abuja", Latitude = 9.0765, Longitude = 7.3986, Country = "Nigeria" },
            new City { Name = "Port Harcourt", Latitude = 4.8156, Longitude = 7.0498, Country = "Nigeria" },

            // Kenya
            new City { Name = "Nairobi", Latitude = -1.2921, Longitude = 36.8219, Country = "Kenya" },
            new City { Name = "Mombasa", Latitude = -4.0435, Longitude = 39.6682, Country = "Kenya" },

            // India
            new City { Name = "New Delhi", Latitude = 28.6139, Longitude = 77.2090, Country = "India" },
            new City { Name = "Mumbai", Latitude = 19.0760, Longitude = 72.8777, Country = "India" },
            new City { Name = "Bangalore", Latitude = 12.9716, Longitude = 77.5946, Country = "India" },
            new City { Name = "Hyderabad", Latitude = 17.3850, Longitude = 78.4867, Country = "India" },
            new City { Name = "Ahmedabad", Latitude = 23.0225, Longitude = 72.5714, Country = "India" },

            // China
            new City { Name = "Beijing", Latitude = 39.9042, Longitude = 116.4074, Country = "China" },
            new City { Name = "Shanghai", Latitude = 31.2304, Longitude = 121.4737, Country = "China" },
            new City { Name = "Guangzhou", Latitude = 23.1291, Longitude = 113.2644, Country = "China" },
            new City { Name = "Shenzhen", Latitude = 22.5431, Longitude = 114.0579, Country = "China" },
            new City { Name = "Chengdu", Latitude = 30.5728, Longitude = 104.0668, Country = "China" },

            // Japan
            new City { Name = "Tokyo", Latitude = 35.6895, Longitude = 139.6917, Country = "Japan" },
            new City { Name = "Osaka", Latitude = 34.6937, Longitude = 135.5023, Country = "Japan" },
            new City { Name = "Nagoya", Latitude = 35.1815, Longitude = 136.9066, Country = "Japan" },
            new City { Name = "Sapporo", Latitude = 43.0621, Longitude = 141.3544, Country = "Japan" },

            // South Korea
            new City { Name = "Seoul", Latitude = 37.5665, Longitude = 126.9780, Country = "South Korea" },
            new City { Name = "Busan", Latitude = 35.1796, Longitude = 129.0756, Country = "South Korea" },
            new City { Name = "Incheon", Latitude = 37.4563, Longitude = 126.7052, Country = "South Korea" },

            // Australia
            new City { Name = "Sydney", Latitude = -33.8688, Longitude = 151.2093, Country = "Australia" },
            new City { Name = "Melbourne", Latitude = -37.8136, Longitude = 144.9631, Country = "Australia" },
            new City { Name = "Brisbane", Latitude = -27.4698, Longitude = 153.0251, Country = "Australia" },
            new City { Name = "Perth", Latitude = -31.9505, Longitude = 115.8605, Country = "Australia" },
            new City { Name = "Adelaide", Latitude = -34.9285, Longitude = 138.6007, Country = "Australia" },

            // New Zealand
            new City { Name = "Auckland", Latitude = -36.8485, Longitude = 174.7633, Country = "New Zealand" },
            new City { Name = "Wellington", Latitude = -41.2865, Longitude = 174.7762, Country = "New Zealand" },
            new City { Name = "Christchurch", Latitude = -43.5321, Longitude = 172.6362, Country = "New Zealand" },

            // UAE
            new City { Name = "Dubai", Latitude = 25.2048, Longitude = 55.2708, Country = "UAE" },
            new City { Name = "Abu Dhabi", Latitude = 24.4539, Longitude = 54.3773, Country = "UAE" },

            // Saudi Arabia
            new City { Name = "Riyadh", Latitude = 24.7136, Longitude = 46.6753, Country = "Saudi Arabia" },
            new City { Name = "Jeddah", Latitude = 21.2854, Longitude = 39.2376, Country = "Saudi Arabia" },
            new City { Name = "Dammam", Latitude = 26.3927, Longitude = 49.9777, Country = "Saudi Arabia" },

            // Qatar
            new City { Name = "Doha", Latitude = 25.2854, Longitude = 51.5310, Country = "Qatar" },

            // Kuwait
            new City { Name = "Kuwait City", Latitude = 29.3759, Longitude = 47.9774, Country = "Kuwait" },

            // Israel
            new City { Name = "Tel Aviv", Latitude = 32.0853, Longitude = 34.7818, Country = "Israel" },
            new City { Name = "Jerusalem", Latitude = 31.7683, Longitude = 35.2137, Country = "Israel" },

            // Jordan
            new City { Name = "Amman", Latitude = 31.9454, Longitude = 35.9284, Country = "Jordan" },
            new City { Name = "Zarqa", Latitude = 32.0695, Longitude = 36.0880, Country = "Jordan" },

            // Lebanon
            new City { Name = "Beirut", Latitude = 33.8938, Longitude = 35.5018, Country = "Lebanon" },

            // Sweden
            new City { Name = "Stockholm", Latitude = 59.3293, Longitude = 18.0686, Country = "Sweden" },
            new City { Name = "Gothenburg", Latitude = 57.7089, Longitude = 11.9746, Country = "Sweden" },
            new City { Name = "Malmö", Latitude = 55.604981, Longitude = 13.003822, Country = "Sweden" },

            // Norway
            new City { Name = "Oslo", Latitude = 59.9139, Longitude = 10.7522, Country = "Norway" },
            new City { Name = "Bergen", Latitude = 60.39299, Longitude = 5.32415, Country = "Norway" },

            // Denmark
            new City { Name = "Copenhagen", Latitude = 55.6761, Longitude = 12.5683, Country = "Denmark" },
            new City { Name = "Aarhus", Latitude = 56.1629, Longitude = 10.2039, Country = "Denmark" },

            // Finland
            new City { Name = "Helsinki", Latitude = 60.1699, Longitude = 24.9384, Country = "Finland" },
            new City { Name = "Tampere", Latitude = 61.4978, Longitude = 23.7610, Country = "Finland" },

            // Poland
            new City { Name = "Warsaw", Latitude = 52.2297, Longitude = 21.0122, Country = "Poland" },
            new City { Name = "Kraków", Latitude = 50.0647, Longitude = 19.9450, Country = "Poland" },
            new City { Name = "Łódź", Latitude = 51.7592, Longitude = 19.4560, Country = "Poland" },

            // Netherlands
            new City { Name = "Amsterdam", Latitude = 52.3676, Longitude = 4.9041, Country = "Netherlands" },
            new City { Name = "Rotterdam", Latitude = 51.9244, Longitude = 4.4777, Country = "Netherlands" },
            new City { Name = "The Hague", Latitude = 52.0705, Longitude = 4.3007, Country = "Netherlands" },
            new City { Name = "Utrecht", Latitude = 52.0907, Longitude = 5.1214, Country = "Netherlands" },

            // Belgium
            new City { Name = "Brussels", Latitude = 50.8503, Longitude = 4.3517, Country = "Belgium" },
            new City { Name = "Antwerp", Latitude = 51.2194, Longitude = 4.4025, Country = "Belgium" },
            new City { Name = "Ghent", Latitude = 51.0543, Longitude = 3.7174, Country = "Belgium" },

            // Switzerland
            new City { Name = "Zurich", Latitude = 47.3769, Longitude = 8.5417, Country = "Switzerland" },
            new City { Name = "Geneva", Latitude = 46.2044, Longitude = 6.1432, Country = "Switzerland" },
            new City { Name = "Basel", Latitude = 47.5596, Longitude = 7.5886, Country = "Switzerland" },

            // Austria
            new City { Name = "Vienna", Latitude = 48.2082, Longitude = 16.3738, Country = "Austria" },
            new City { Name = "Graz", Latitude = 47.0707, Longitude = 15.4395, Country = "Austria" },
            new City { Name = "Linz", Latitude = 48.3069, Longitude = 14.2858, Country = "Austria" },

            // Portugal
            new City { Name = "Lisbon", Latitude = 38.7223, Longitude = -9.1393, Country = "Portugal" },
            new City { Name = "Porto", Latitude = 41.1579, Longitude = -8.6291, Country = "Portugal" },

            // Greece
            new City { Name = "Athens", Latitude = 37.9838, Longitude = 23.7275, Country = "Greece" },
            new City { Name = "Thessaloniki", Latitude = 40.6401, Longitude = 22.9444, Country = "Greece" },

            // Czech Republic
            new City { Name = "Prague", Latitude = 50.0755, Longitude = 14.4378, Country = "Czech Republic" },
            new City { Name = "Brno", Latitude = 49.1951, Longitude = 16.6068, Country = "Czech Republic" },

            // Hungary
            new City { Name = "Budapest", Latitude = 47.4979, Longitude = 19.0402, Country = "Hungary" },
            new City { Name = "Debrecen", Latitude = 47.5316, Longitude = 21.6273, Country = "Hungary" },

            // Romania
            new City { Name = "Bucharest", Latitude = 44.4268, Longitude = 26.1025, Country = "Romania" },
            new City { Name = "Cluj-Napoca", Latitude = 46.7712, Longitude = 23.6236, Country = "Romania" },

            // Bulgaria
            new City { Name = "Sofia", Latitude = 42.6977, Longitude = 23.3219, Country = "Bulgaria" },
            new City { Name = "Plovdiv", Latitude = 42.1354, Longitude = 24.7453, Country = "Bulgaria" },

            // Croatia
            new City { Name = "Zagreb", Latitude = 45.8150, Longitude = 15.9819, Country = "Croatia" },
            new City { Name = "Split", Latitude = 43.5081, Longitude = 16.4402, Country = "Croatia" },

            // Slovakia
            new City { Name = "Bratislava", Latitude = 48.1486, Longitude = 17.1077, Country = "Slovakia" },

            // Slovenia
            new City { Name = "Ljubljana", Latitude = 46.0569, Longitude = 14.5058, Country = "Slovenia" },

            // Serbia
            new City { Name = "Belgrade", Latitude = 44.7866, Longitude = 20.4489, Country = "Serbia" },
            new City { Name = "Niš", Latitude = 43.3209, Longitude = 21.8958, Country = "Serbia" },

            // Ukraine
            new City { Name = "Kyiv", Latitude = 50.4501, Longitude = 30.5234, Country = "Ukraine" },
            new City { Name = "Kharkiv", Latitude = 49.9935, Longitude = 36.2304, Country = "Ukraine" },
            new City { Name = "Odesa", Latitude = 46.4825, Longitude = 30.7233, Country = "Ukraine" },

            // Iran
            new City { Name = "Tehran", Latitude = 35.6892, Longitude = 51.3890, Country = "Iran" },
            new City { Name = "Mashhad", Latitude = 36.2605, Longitude = 59.6168, Country = "Iran" },

            // Pakistan
            new City { Name = "Karachi", Latitude = 24.8607, Longitude = 67.0011, Country = "Pakistan" },
            new City { Name = "Lahore", Latitude = 31.5204, Longitude = 74.3587, Country = "Pakistan" },
            new City { Name = "Islamabad", Latitude = 33.6844, Longitude = 73.0479, Country = "Pakistan" },

            // Bangladesh
            new City { Name = "Dhaka", Latitude = 23.8103, Longitude = 90.4125, Country = "Bangladesh" },
            new City { Name = "Chittagong", Latitude = 22.3569, Longitude = 91.7832, Country = "Bangladesh" },

            // Indonesia
            new City { Name = "Jakarta", Latitude = -6.2088, Longitude = 106.8456, Country = "Indonesia" },
            new City { Name = "Surabaya", Latitude = -7.2575, Longitude = 112.7521, Country = "Indonesia" },
            new City { Name = "Bandung", Latitude = -6.9175, Longitude = 107.6191, Country = "Indonesia" },

            // Vietnam
            new City { Name = "Hanoi", Latitude = 21.0278, Longitude = 105.8342, Country = "Vietnam" },
            new City { Name = "Ho Chi Minh City", Latitude = 10.8231, Longitude = 106.6297, Country = "Vietnam" },

            // Malaysia
            new City { Name = "Kuala Lumpur", Latitude = 3.1390, Longitude = 101.6869, Country = "Malaysia" },
            new City { Name = "George Town", Latitude = 5.4141, Longitude = 100.3288, Country = "Malaysia" },

            // Singapore
            new City { Name = "Singapore", Latitude = 1.3521, Longitude = 103.8198, Country = "Singapore" },

            // Philippines
            new City { Name = "Manila", Latitude = 14.5995, Longitude = 120.9842, Country = "Philippines" },
            new City { Name = "Cebu City", Latitude = 10.3157, Longitude = 123.8854, Country = "Philippines" },

            // Thailand
            new City { Name = "Bangkok", Latitude = 13.7563, Longitude = 100.5018, Country = "Thailand" },
            new City { Name = "Chiang Mai", Latitude = 18.7061, Longitude = 98.9817, Country = "Thailand" },

            // South Korea (already more than 2 in raw list)
            new City { Name = "Seoul", Latitude = 37.5665, Longitude = 126.9780, Country = "South Korea" },
            new City { Name = "Busan", Latitude = 35.1796, Longitude = 129.0756, Country = "South Korea" },
            new City { Name = "Incheon", Latitude = 37.4563, Longitude = 126.7052, Country = "South Korea" }
        ];
        return cities;
    }

}
