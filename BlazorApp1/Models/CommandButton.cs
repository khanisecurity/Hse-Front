using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Models
{
    public class CommandButton
    {
        public string Command { get; set; }
        public string Icon { get; set; }
        public Action<object> OnClick { get; set; }
        public string DisplayName { get; set; }
    }
}
