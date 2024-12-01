using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BlazorApp1.Models.Products;

namespace BlazorApp1.Models.Invoice
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public Product Product { get; set; }
        public Unit Unit { get; set; }
    }
}
