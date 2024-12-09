namespace BlazorApp1.Service.Contracts
{
    public interface IStorageService
    {
        Task SetItem(string key, string? value, bool persistent = true);

        Task<string?> GetItem(string key);

        Task<bool> IsPersistent(string key);

        Task RemoveItem(string key);
    }
}
