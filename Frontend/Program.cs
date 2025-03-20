using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Frontend.Services;
using GoogleMapsComponents;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BackendAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5039/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddFluentUIComponents();
builder.Services.AddBlazorGoogleMaps("AIzaSyDP62fj - DP2jv8zbN84gMfF08gCCO3Fai4");

builder.Services.AddScoped<ApiService>();

await builder.Build().RunAsync();
