using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace BlazorApp1.Service
{
    public class CultureService
    {
        public void SetCultureAsync(string culture)
        {
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture));
            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                cookieValue);

            //await jsRuntime.InvokeVoidAsync("location.reload");
        }

        public void ChangeLanguage(string culture)
        {
            SetCultureAsync(culture);
        }

        //public static IActionResult ResetCulture(string redirectUri)
        //{
        //    var httpContextAccessor = new HttpContextAccessor();
        //    httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);

        //    //return LocalRedirect(redirectUri);
        //}
    }
}