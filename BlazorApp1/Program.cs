using BlazorApp1;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1.Data;
using BlazorApp1.DbContext;
using System.Globalization;
using Telerik.Blazor.Services;
using BlazorApp1.Services;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#layout-wrapper");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddSingleton<ApplicationDbContext>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddTelerikBlazor();
builder.Services.AddMudServices();


// register a custom localizer for the Telerik components, after registering the Telerik services
builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(ResxLocalizer));
var host = builder.Build();

#region Localization
const string defaultCulture = "en-US";
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");
var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);

if (result == null)
    await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

#endregion
await host.RunAsync();