
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.Blazor.Services;
using TelerikBlazorApp1.Client;
using TelerikBlazorApp1.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTelerikBlazor();

// register a custom localizer for the Telerik components, after registering the Telerik services
builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(ResxLocalizer));

var host = builder.Build();

await SetCultureAsync(host);

await host.RunAsync();

// based on https://github.com/pranavkm/LocSample
static async Task SetCultureAsync(WebAssemblyHost host)
{
    var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
    var cultureName = await jsRuntime.InvokeAsync<string>("blazorCulture.get");

    if (cultureName != null)
    {
        var culture = new CultureInfo(cultureName);

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
