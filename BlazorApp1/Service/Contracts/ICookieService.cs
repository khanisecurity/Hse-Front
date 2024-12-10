namespace BlazorApp1.Service.Contracts
{
    public interface ICookieService
    {
        Task SetCookie(ButilCookie cookie);
        Task<ButilCookie?> GetCookie(string key);
        Task DeleteCookie(string key);
    }
}
