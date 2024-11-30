using TelerikGrid.Models.Products;

namespace TelerikGrid.DbContext
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        List<Unit> GetUnitsByProductIdAsync(int productId);
    }
}
