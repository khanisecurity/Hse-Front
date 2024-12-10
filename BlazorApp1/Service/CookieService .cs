using BlazorApp1.Service.Contracts;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace BlazorApp1.Service
{
    public enum SameSite
    {
        //
        // Summary:
        //     Explicitly states no restrictions will be applied. The cookie will be sent in
        //     all requests—both cross-site and same-site.
        None,
        //
        // Summary:
        //     Send the cookie for all same-site requests and top-level navigation GET requests.
        Lax,
        //
        // Summary:
        //     Prevent the cookie from being sent by the browser to the target site in all cross-site
        //     browsing contexts, even when following a regular link.
        Strict
    }
    public class ButilCookie
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Domain { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public long? MaxAge { get; set; }
        public bool Partitioned { get; set; }
        public string? Path { get; set; }
        public SameSite? SameSite { get; set; }
        public bool Secure { get; set; }

        public override string ToString()
        {
            if (Name is null) return string.Empty;

            var sb = new StringBuilder();

            sb.Append($"{Name}={Value}");

            if (Domain is not null)
            {
                sb.Append($";domain={Domain}");
            }

            if (Expires is not null)
            {
                sb.Append($";expires={Expires.Value.UtcDateTime.ToString("ddd, MMM dd yyyy HH:mm:ss \"GMT\"")}");
            }

            if (MaxAge is not null)
            {
                sb.Append($";max-age={MaxAge}");
            }

            if (Partitioned)
            {
                sb.Append(";partitioned");
            }

            if (Path is not null)
            {
                sb.Append($";path={Path}");
            }

            if (SameSite is not null)
            {
                sb.Append($";samesite={SameSite.ToString()!.ToLowerInvariant()}");
            }

            if (Secure)
            {
                sb.Append(";secure");
            }

            return sb.ToString();
        }

        public static ButilCookie Parse(string rawCookie)
        {
            ButilCookie butilCookie = new ButilCookie();
            if (rawCookie.Contains('='))
            {
                string[] array = rawCookie.Split('=');
                butilCookie.Name = array[0].Trim();
                butilCookie.Value = array[1].Trim();
            }

            return butilCookie;
        }
    }
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SetCookie(ButilCookie cookie)
        {
            if (cookie.Name == null)
                throw new ArgumentNullException(nameof(cookie.Name), "Cookie name cannot be null.");

            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = cookie.Secure,
                Path = cookie.Path ?? "/",
                SameSite = (cookie.SameSite.ToString() == SameSiteMode.Strict.ToString()) ? Enum.Parse<SameSiteMode>(cookie.SameSite.ToString()) : SameSiteMode.None
            };

            if (cookie.Expires.HasValue)
            {
                options.Expires = cookie.Expires.Value.UtcDateTime;
            }

            if (cookie.Domain != null)
            {
                options.Domain = cookie.Domain;
            }

            string cookieValue = JsonSerializer.Serialize(cookie);
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookie.Name, cookieValue, options);
        }

        public async Task<ButilCookie?> GetCookie(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Cookie name cannot be null.");

            var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies[name];

            if (string.IsNullOrEmpty(cookieValue))
                return null;

            return JsonSerializer.Deserialize<ButilCookie>(cookieValue);
        }

        public async Task DeleteCookie(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Cookie name cannot be null.");

            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(name);
        }
    }
}
