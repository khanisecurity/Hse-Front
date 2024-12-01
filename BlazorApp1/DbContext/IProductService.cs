using BlazorApp1.Models.Products;

namespace BlazorApp1.DbContext
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        List<Unit> GetUnitsByProductIdAsync(int productId);
    }
}
