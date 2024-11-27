using System.ComponentModel.DataAnnotations;

namespace farmacius.Models
{
    public class vProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class SaleViewModel
    {
        public List<vProduct> vProducts { get; set; } = new List<vProduct>();
        public List<CartItem> Cart { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; set; }
    }
}
