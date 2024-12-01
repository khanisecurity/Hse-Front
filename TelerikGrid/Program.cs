using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Telerik.Blazor.Services;
using TelerikGrid.Data;
using TelerikGrid.DbContext;
using TelerikGrid.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
    options.DetailedErrors = true;
});
builder.Services.AddTelerikBlazor();
builder.Services.AddMudServices();

builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddSingleton<ApplicationDbContext>();


// register a custom localizer for the Telerik components, after registering the Telerik services
builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(ResxLocalizer));

#region Localization

builder.Services.AddControllers();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // define the list of cultures your app will support
    var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("en-US"),
                new CultureInfo("de-DE"),
                new CultureInfo("es-ES"),
                new CultureInfo("bg-BG"),
                new CultureInfo("fa-IR")

            };

     // set the default culture
    options.DefaultRequestCulture = new RequestCulture("fa-IR");

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// the custom localizer service is registered later, after the Telerik services

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region Localization

app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);

#endregion

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
//app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();



