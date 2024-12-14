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

builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<ICookieService, CookieService>();

builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(ResxLocalizer));

builder.Services.AddBlazoredLocalStorage();


var host = builder.Build();

#region Localization with local storage
//const string defaultCulture = "en-US";
//var js = host.Services.GetRequiredService<IJSRuntime>();
//var result = await js.InvokeAsync<string>("blazorCulture.get");
//var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);

//if (result == null)
//    await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);

//CultureInfo.DefaultThreadCurrentCulture = culture;
//CultureInfo.DefaultThreadCurrentUICulture = culture;


const string defaultCulture = "en-US";
var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
StorageService._localStorage = localStorage;
var storedCulture = await localStorage.GetItemAsync<string>("appCulture");
var cultureName = string.IsNullOrEmpty(storedCulture) ? defaultCulture : storedCulture;
var culture = CultureInfo.GetCultureInfo(cultureName);
Thread.CurrentThread.CurrentCulture = culture;
Thread.CurrentThread.CurrentUICulture = culture;

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
if (string.IsNullOrEmpty(storedCulture))
{
    await localStorage.SetItemAsync("language", defaultCulture);
}
#endregion

await host.RunAsync();