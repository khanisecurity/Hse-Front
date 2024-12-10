using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Services;
using BlazorApp1.Resources;
using BlazorApp1.Service;
using Microsoft.Extensions.Localization;
using BlazorApp1.Service.Contracts;
using Microsoft.JSInterop;
namespace BlazorApp1.Pages
{
    public class AppComponentBase : ComponentBase
    {
        #region Dep Injections 
        /* [AutoInject] for future approaches */
        [Inject] protected ITelerikStringLocalizer Localizer { get; set; }
        [Inject] protected CultureInfoManager CultureManageer { get; set; }
        [Inject] protected IStorageService StorageService { get; set; }
        [Inject] protected ICookieService cookieService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }
        #endregion
     
        #region Potentioal change to the defualt Initial functions
        protected sealed override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected sealed override async Task OnInitializedAsync()
        {
            try
            {
                await OnInitAsync();
                await base.OnInitializedAsync();
            }
            catch (Exception exp)
            {
                // ExceptionHandler.Handle(exp);
                Console.WriteLine(exp.Message);
            }
        }
        protected virtual Task OnInitAsync()
        {
            return Task.CompletedTask;
        }
        protected sealed override async Task OnParametersSetAsync()
        {
            try
            {
                await OnParamsSetAsync();
                await base.OnParametersSetAsync();
            }
            catch (Exception exp)
            {
                //ExceptionHandler.Handle(exp);
                Console.WriteLine(exp.Message);
            }
        }
        protected virtual Task OnParamsSetAsync()
        {
            return Task.CompletedTask;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    await OnAfterFirstRenderAsync();
                }
                catch (Exception exp)
                {
                    //ExceptionHandler.Handle(exp);
                    Console.WriteLine(exp.Message);
                }
            }

            await base.OnAfterRenderAsync(firstRender);
        }
        protected virtual Task OnAfterFirstRenderAsync()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Exception Handling Approach

        public virtual Action WrapHandled(Action action)
        {
            return () =>
            {
                try
                {
                    action();
                }
                catch (Exception exp)
                {
                    //ExceptionHandler.Handle(exp);
                    Console.WriteLine(exp.Message);
                }
            };
        }
        public virtual Action<T> WrapHandled<T>(Action<T> func)
        {
            return (e) =>
            {
                try
                {
                    func(e);
                }
                catch (Exception exp)
                {
                    //ExceptionHandler.Handle(exp);
                    Console.WriteLine(exp.Message);
                }
            };
        }
        public virtual Func<Task> WrapHandled(Func<Task> func)
        {
            return async () =>
            {
                try
                {
                    await func();
                }
                catch (Exception exp)
                {
                    //ExceptionHandler.Handle(exp);
                    Console.WriteLine(exp.Message);
                }
            };
        }
        public virtual Func<T, Task> WrapHandled<T>(Func<T, Task> func)
        {
            return async (e) =>
            {
                try
                {
                    await func(e);
                }
                catch (Exception exp)
                {
                    //ExceptionHandler.Handle(exp);
                    Console.WriteLine(exp.Message);
                }
            };
        }
        #endregion
    }
}