using BlazorApp1.Models.Invoice;
using BlazorApp1.Models.Products;

namespace BlazorApp1.Data
{
    public interface IDataService
    {
        List<Invoice> GetInvoices();
        List<Product> GetProducts();
        void  CreateInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        void DeleteInvoice(int id);
        List<Unit> LoadUnitsByProductId(int productId);
    }
}
