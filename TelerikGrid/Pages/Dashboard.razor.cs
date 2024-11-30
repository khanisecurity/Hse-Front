using Telerik.Blazor.Components;
using TelerikGrid.Models.Invoice;
using static System.Net.WebRequestMethods;
using TelerikGrid.Models.Products;
using Microsoft.AspNetCore.Components;

namespace TelerikGrid.Pages
{
    public partial class Dashboard
    {
        
        private List<Invoice> GridData = new List<Invoice>();
        private List<Product> Products = new List<Product>();
        private List<Unit> FilteredUnits = new List<Unit>();
        private int selectedProductId { get; set; }

        public List<int?> PageSizes { get; } = new List<int?> { 5, 10, 15, 20, null };
        private int PageSize { get; set; } = 10;
        private int CurrentPage { get; set; } = 1;

        private bool ExportAllPages { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoadData();
        }

        private void LoadData()
        {
            GridData = _dataService.GetInvoices();
            Products = _dataService.GetProducts();
        }

        private async Task LoadUnits(int productId)
        {
            var units = _dataService.LoadUnitsByProductId(productId);
            FilteredUnits = units;
            StateHasChanged(); // بروزرسانی صفحه  
        }

        private void ItemResize()
        {
            StateHasChanged();
        }

        private void CreateItem(GridCommandEventArgs args)
        {
            _dataService.CreateInvoice((Invoice)args.Item);
            LoadData();
        }

        private void DeleteItem(GridCommandEventArgs args)
        {
            //await _dataService.DeleteInvoice((Invoice)args.Item);
            LoadData();
        }

        private void UpdateItem(GridCommandEventArgs args)
        {
            _dataService.UpdateInvoice((Invoice)args.Item);
            LoadData();
        }

        private void OnProductChanged(object theUserInput)
        {
            selectedProductId = Convert.ToInt32(theUserInput);
            //if (int.TryParse(e.Value?.ToString(), out var selectedProductId))
            LoadUnits(selectedProductId);
        }
    }
}