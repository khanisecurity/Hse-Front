using TelerikGrid.Models.Products;

namespace TelerikGrid.DbContext
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return _context.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Units = _context.Units.Where(u => u.ProductId == p.Id).ToList() 
            }).ToList();
        }

        public List<Unit> GetUnitsByProductIdAsync(int productId)
        {
            return _context.Units.Where(u => u.ProductId == productId).ToList();
        }
    }
}
