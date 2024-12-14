using System.Globalization;

namespace BlazorApp1.Pages.Components.HeaderMenu;

public partial class Language
{
    private record LanguageT(string Culture, string Name, string FlagUrl);
    private List<LanguageT> Languages { get; set; } = new()
    {
        new LanguageT("en-US", "English", "/images/flags/us.svg"),
        new LanguageT("es-ES", "Spanish", "/images/flags/spain.svg"),
        new LanguageT("de-DE", "German", "/images/flags/germany.svg"),
        new LanguageT("it-IT", "Italian", "/images/flags/italy.svg"),
        new LanguageT("ru-RU", "Russian", "/images/flags/russia.svg"),
        new LanguageT("zh-CN", "Chinese", "/images/flags/china.svg"),
        new LanguageT("fr-FR", "French", "/images/flags/french.svg"),
        new LanguageT("ar-SA", "Arabic", "/images/flags/ae.svg"),
        new LanguageT("fa-IR", "Farsi", "/images/flags/ir.svg"),
    };

    private LanguageT SelectedLanguage { get; set; } = new("fa-IR", "Farsi", "/images/flags/ir.svg");

    private CultureInfo? SelectedCulture;
    protected override async Task OnInitAsync()
    {

        await Task.Run(() =>
        {
            Console.WriteLine(CultureInfo.CurrentCulture);
            SelectedCulture = Thread.CurrentThread.CurrentCulture;
            SelectedLanguage = Languages.Where(x => x.Culture == SelectedCulture.ToString()).First();
        });

    }

    private async Task ChangeLanguage(string culture)
    {
        Console.WriteLine(Navigation.Uri);
        SelectedLanguage = Languages.Where(x => x.Culture == culture).First();
        SelectedCulture = CultureInfo.GetCultureInfo((Languages.FirstOrDefault(l => l.Culture == culture) ?? SelectedLanguage).Culture);
        await StorageService.SetItem("appCulture", culture, persistent: true);
        CultureManageer.SetCurrentCulture(SelectedCulture!.ToString());
        Console.WriteLine(Navigation.Uri);
        Navigation.NavigateTo(Navigation.Uri, true);

        // the Cookie service needs rework!


        // if (AppPlatform.IsBlazorHybrid)
        // {
        // }
        // else
        // {
        //     await cookieService.SetCookie(new()
        //         {
        //             Name = ".AspNetCore.Culture",
        //             Value = Uri.EscapeDataString($"c={SelectedCulture}|uic={SelectedCulture}"),
        //             MaxAge = 30 * 24 * 3600,
        //             Secure = AppEnvironment.IsDev() is false
        //         });
        // }

    }
}
