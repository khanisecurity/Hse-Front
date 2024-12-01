namespace BlazorApp1.Models.Products
{

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Unit> Units { get; set; } 
    }
}
