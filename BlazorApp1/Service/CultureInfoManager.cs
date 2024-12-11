using System.Globalization;
using System.Runtime.CompilerServices;
using Telerik.SvgIcons;

namespace BlazorApp1.Service;

public class CultureInfoManager 
{

    public static CultureInfo DefaultCulture => CreateCultureInfo("fa-IR");
    public static (string DisplayName, CultureInfo Culture)[] SupportedCultures =>
    [
        ("English", CreateCultureInfo("en-US")),
        ("Española", CreateCultureInfo("es-ES")),
        ("Deutsche", CreateCultureInfo("de-DE")),
        ("Italiana", CreateCultureInfo("it-IT")),
        ("русский", CreateCultureInfo("ru-RU")),
        ("中国人", CreateCultureInfo("zh-CN")),
        ("Française", CreateCultureInfo("fr-FR")),
        ("Arabic", CreateCultureInfo("ar-AE")),
        ("Farsi", CreateCultureInfo("fa-IR"))
    ];

    public static CultureInfo CreateCultureInfo(string name)
    {
        var cultureInfo = OperatingSystem.IsBrowser() ? CultureInfo.CreateSpecificCulture(name) : new CultureInfo(name);

        if (name == "fa-IR")
        {
            CustomizeCultureInfoForFaCulture(cultureInfo);
        }

        return cultureInfo;
    }
    public void SetCurrentCulture(string cultureName)
    {
        var cultureInfo = SupportedCultures.FirstOrDefault(sc => sc.Culture.Name == cultureName).Culture ?? DefaultCulture;

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

    }
    private static CultureInfo CustomizeCultureInfoForFaCulture(CultureInfo cultureInfo)
    {
        cultureInfo.DateTimeFormat.AMDesignator = "ق.ظ";
        cultureInfo.DateTimeFormat.PMDesignator = "ب.ظ";
        cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        cultureInfo.DateTimeFormat.AbbreviatedDayNames =
        [
            "ی", "د", "س", "چ", "پ", "ج", "ش"
        ];
        cultureInfo.DateTimeFormat.ShortestDayNames =
        [
            "ی", "د", "س", "چ", "پ", "ج", "ش"
        ];

        Get_CalendarField(cultureInfo) = new PersianCalendar();

        return cultureInfo;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_calendar")]
    extern static ref System.Globalization.Calendar Get_CalendarField(CultureInfo cultureInfo);
}
