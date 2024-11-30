using TelerikGrid.Models.Invoice;
using TelerikGrid.Models.Products;

namespace TelerikGrid.Data
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
