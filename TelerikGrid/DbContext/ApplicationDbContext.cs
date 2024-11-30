using System.Collections.Generic;
using System.Reflection.Emit;
using TelerikGrid.Models.Invoice;
using TelerikGrid.Models.Products;

namespace TelerikGrid.DbContext
{
    public class ApplicationDbContext 
    {
        public List<Product> Products { get; set; } = new();
        public List<Unit> Units { get; set; } = new();
        public List<Invoice> Invoices { get; set; } = new();

        public ApplicationDbContext()
        {
            SeedData();  // پر کردن داده‌های ابتدایی  
        }

        private void SeedData()
        {
            // افزودن نمونه کالاها  
            var product1 = new Product { Id = 1, Name = "Product 1", Units= new List<Unit>() };
            var product2 = new Product { Id = 2, Name = "Product 2" , Units = new List<Unit>() };

            // افزودن نمونه واحدها  
            var unit1 = new Unit { Id = 1, Name = "Unit 1", ProductId = 1 };
            var unit2 = new Unit { Id = 2, Name = "Unit 2", ProductId = 1 };
            var unit3 = new Unit { Id = 3, Name = "Unit 3", ProductId = 2 };

            product1.Units.Add(unit1);
            product1.Units.Add(unit2);
            product2.Units.Add(unit3);

            Products.Add(product1);
            Products.Add(product2);
            Units.Add(unit1);
            Units.Add(unit2);
            Units.Add(unit3);
        }

        // متدهای CRUD  
        public void AddProduct(Product product) => Products.Add(product);
        public void UpdateProduct(Product product)
        {
            var existing = Products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Units = product.Units; // اینجا می‌توانید روش خاصی برای بروزرسانی واحدها پیاده‌سازی کنید  
            }
        }

        public void DeleteProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                Products.Remove(product);
            }
        }

        public void AddUnit(Unit unit) => Units.Add(unit);
        public void UpdateUnit(Unit unit)
        {
            var existing = Units.FirstOrDefault(u => u.Id == unit.Id);
            if (existing != null)
            {
                existing.Name = unit.Name;
                existing.ProductId = unit.ProductId;
            }
        }

        public void DeleteUnit(int unitId)
        {
            var unit = Units.FirstOrDefault(u => u.Id == unitId);
            if (unit != null)
            {
                Units.Remove(unit);
            }
        }

        public void AddInvoice(Invoice invoice) => Invoices.Add(invoice);
    }
}
