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
using BlazorApp1.Service;
using BlazorApp1.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#layout-wrapper");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddSingleton<ApplicationDbContext>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddTelerikBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<CultureInfoManager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// builder.Services.AddSingleton<IStorageService, StorageService>(); not a real dp inj

// register a custom localizer for the Telerik components, after registering the Telerik services
builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(ResxLocalizer));

builder.Services.AddBlazoredLocalStorage();


var host = builder.Build();

#region Localization with local storage

const string defaultCulture = "en-US";
var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
StorageService._localStorage = localStorage;
var storedCulture = await localStorage.GetItemAsync<string>("appCulture");
var cultureName = string.IsNullOrEmpty(storedCulture) ? defaultCulture : storedCulture;
var culture = CultureInfo.GetCultureInfo(cultureName);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
if (string.IsNullOrEmpty(storedCulture))
{
    await localStorage.SetItemAsync("appCulture", defaultCulture);
}
#endregion

await host.RunAsync();