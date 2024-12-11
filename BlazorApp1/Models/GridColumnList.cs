using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Models
{
    public class GridColumnList
    {
        public string Field { get; set; } // نام فیلدی که به آن اشاره می‌کند  
        public string Title { get; set; } // عنوان ستون  
        public string Width { get; set; }  // عرض ستون (اختیاری)  
        public bool IsVisible { get; set; } = true;  // تعیین قابل نمایش بودن ستون  
        public bool IsActionColumn { get; set; } = false;  // مشخص می‌کند آیا این ستون شامل دکمه‌های عملیات است  
        public List<CommandButton> ActionButtons { get; set; } = new List<CommandButton>(); // لیست دکمه‌های عملیات  
    }
}
