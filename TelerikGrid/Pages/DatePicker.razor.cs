﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Reflection;

namespace TelerikGrid.Pages
{
    public partial class DatePicker 
    {
        private DateTime? _date = new DateTime(2024, 02, 14); // 1399-11-26 in Persian calendar

        public CultureInfo GetPersianCulture()
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
            Calendar cal = new PersianCalendar();
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
    }
}
