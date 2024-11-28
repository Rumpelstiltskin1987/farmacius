using System.ComponentModel.DataAnnotations;

namespace farmacius.Models
{
    public class SaleViewModel
    {
        public List<Product> vProducts { get; set; } = new List<Product>();
        public List<CartItem> Cart { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; set; }
    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }


}
