namespace BlazorApp1.Models.Products
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; } 
        public Product Product { get; set; }
    }
}
