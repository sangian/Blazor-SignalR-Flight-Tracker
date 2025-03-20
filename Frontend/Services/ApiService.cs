using System.Text;

namespace Frontend.Services;

public class ApiService
{
    private readonly IHttpClientFactory httpClientFactory;

    public ApiService(IHttpClientFactory HttpClientFactory)
    {
        httpClientFactory = HttpClientFactory;
    }

    public async Task StartAirplane(int airplaneId)
    {
        var httpClient = httpClientFactory.CreateClient("BackendAPI");

        await httpClient.PostAsync($"api/SignalR/airplanes/{airplaneId}/start", 
            new StringContent(string.Empty, Encoding.UTF8, "application/json"));

    }

    public async Task StopAirplane(int airplaneId)
    {
        var httpClient = httpClientFactory.CreateClient("BackendAPI");

        await httpClient.PostAsync($"api/SignalR/airplanes/{airplaneId}/stop", 
            new StringContent(string.Empty, Encoding.UTF8, "application/json"));

    }
}
