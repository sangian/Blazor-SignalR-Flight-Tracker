using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Frontend.Services;
using GoogleMapsComponents;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var BackendApiBaseUrl = builder.Configuration["BackendApi:BaseUrl"];
builder.Services.AddHttpClient("BackendAPI", client =>
{
    client.BaseAddress = new Uri(BackendApiBaseUrl!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddFluentUIComponents();

var apiKey = builder.Configuration["GoogleMaps:ApiKey"];
builder.Services.AddBlazorGoogleMaps(apiKey!);

builder.Services.AddScoped<ApiService>();

await builder.Build().RunAsync();
