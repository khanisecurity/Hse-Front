using System.Globalization;
using System.Reflection;

namespace BlazorApp1.Utility
{
    public static class DateUtility
    {
        public static CultureInfo GetPersianCulture()
        {
            var culture = new CultureInfo("fa-IR");
            DateTimeFormatInfo formatInfo = culture.DateTimeFormat;
            formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
            var monthNames = new[]
            {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
            "اسفند",
            "",
        };
            formatInfo.AbbreviatedMonthNames =
                formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
            formatInfo.AMDesignator = "ق.ظ";
            formatInfo.PMDesignator = "ب.ظ";
            formatInfo.ShortDatePattern = "yyyy/MM/dd";
            formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
            formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
            System.Globalization.Calendar cal = new PersianCalendar();
            FieldInfo fieldInfo = culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
                fieldInfo.SetValue(culture, cal);
            FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (info != null)
                info.SetValue(formatInfo, cal);
            culture.NumberFormat.NumberDecimalSeparator = "/";
            culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
            culture.NumberFormat.NumberNegativePattern = 0;
            return culture;
        }

        public static CultureInfo GetArabicCulture()
        {
            var culture = new CultureInfo("ar-SA");
            DateTimeFormatInfo formatInfo = culture.DateTimeFormat;

            // نام روزها
            formatInfo.AbbreviatedDayNames = new[] { "أ", "ث", "ر", "خ", "ج", "س", "ح" };
            formatInfo.DayNames = new[] { "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت" };

            // نام ماه‌ها
            var monthNames = new[]
            {
        "محرم", "صفر", "ربيع الأول", "ربيع الآخر", "جمادى الأولى", "جمادى الآخرة",
        "رجب", "شعبان", "رمضان", "شوال", "ذو القعدة", "ذو الحجة", ""
    };

            formatInfo.AbbreviatedMonthNames =
                formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;

            formatInfo.AMDesignator = "ص";
            formatInfo.PMDesignator = "م";
            formatInfo.ShortDatePattern = "yyyy/MM/dd";
            formatInfo.LongDatePattern = "dddd، dd MMMM، yyyy";
            formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;

            // استفاده از تقویم هجری
            var cal = new HijriCalendar();
            FieldInfo fieldInfo = culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
                fieldInfo.SetValue(culture, cal);

            FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (info != null)
                info.SetValue(formatInfo, cal);

            culture.NumberFormat.NumberDecimalSeparator = ".";
            culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
            culture.NumberFormat.NumberNegativePattern = 0;

            return culture;
        }

        public static DateTime ConvertPersianToGregorian(DateTime persianDate)
        {
            PersianCalendar p = new PersianCalendar();
            DateTime x = p.ToDateTime(persianDate.Year, persianDate.Month, persianDate.Day, persianDate.Hour, persianDate.Minute, persianDate.Second, persianDate.Millisecond);
            return x;
        }
    }
}
