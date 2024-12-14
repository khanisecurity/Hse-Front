using BlazorApp1.Pages;
using BlazorApp1.Service.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace BlazorApp1.Service
{
    public class StorageService : IStorageService
    {
        public static ILocalStorageService _localStorage { get; set; }
        [Inject] ISyncLocalStorageService _sessionStorage { get; set; } = default!;

        private async Task<T?> GetStorageAsync<T>(bool persistent, string key)
        {
            return persistent
                ? await _localStorage.GetItemAsync<T>(key)
                : _sessionStorage.GetItem<T>(key);
        }

        public async Task SetStorageAsync<T>(bool persistent, string key, T value)
        {
            if (persistent)
            {
                await _localStorage.SetItemAsync(key, value);
            }
            else
            {
                _sessionStorage.SetItem(key, value);
            }
        }
        public async Task SetItem(string key, string? value, bool persistent = true)
        {
            if (persistent)
            {
                await _localStorage.SetItemAsync(key, value);
            }
            else
            {
                _sessionStorage.SetItem(key, value);
            }
        }
        public async Task<string?> GetItem(string key)
        {
            var localValue = await _localStorage.GetItemAsync<string?>(key);
            if (localValue != null)
            {
                return localValue;
            }
            return _sessionStorage.GetItem<string?>(key);
        }
        public async Task RemoveItem(string key)
        {
            await _localStorage.RemoveItemAsync(key);
            _sessionStorage.RemoveItem(key);
        }
        public async Task<bool> IsPersistent(string key)
        {
            var localValue = await _localStorage.GetItemAsync<string?>(key);
            return localValue is not null;
        }
    }
}