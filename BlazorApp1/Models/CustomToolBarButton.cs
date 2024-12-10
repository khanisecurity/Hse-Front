using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Models
{
    public class CustomToolBarButton
    {
        public string Command { get; set; } // فرمان دکمه  
        public string Icon { get; set; }     // آیکون دکمه  
        public Action<object> OnClick { get; set; } // رویداد کلیک دکمه  
        public string DisplayName { get; set; } // نام نمایشی دکمه  
    }
}
