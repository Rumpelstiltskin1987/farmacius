using farmacius.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace farmacius.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<Product> productsV = new List<Product>
        {
            new Product { Id = 1, Name = "Paracetamol", Description = "Paracetamol", Price = 10.00m, Quantity = 10, ImageUrl="/images/paracetamol.jpg" },
            new Product { Id = 2, Name = "Loratadina", Description = "Loratadina 2", Price = 20.00m, Quantity = 5, ImageUrl="/images/loratadina.jpg" },
            new Product { Id = 3, Name = "Desloratadina", Description = "Desloratadina 2", Price = 15.00m, Quantity = 5, ImageUrl="/images/desloratadina.jpg" },
            new Product { Id = 4, Name = "Ibuprofeno", Description = "Ibuprofeno 2", Price = 18.00m, Quantity = 5, ImageUrl="/images/ibuprofeno.jpg" }
        };

        private static List<CartItem> cart = new List<CartItem>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Products(Login model) {

            if (ModelState.IsValid)
            {
                ProductViewModel products = new ProductViewModel();

                products.Products = productsV;

                return View("Index", products);
            }
            else
            {
                return View("Login");
            }
        }

        public IActionResult ProductList(Login model)
        {
            ProductViewModel products = new ProductViewModel();

            products.Products = productsV;

            return View("Index", products);
        }

        public IActionResult Alta()
        {
            Models.ProductViewModel products = new Models.ProductViewModel();
            return View(products);
        }

        #region Venta

        [HttpGet]
        public IActionResult Venta()
        {
            var model = new SaleViewModel
            {
                vProducts = productsV,
                Cart = cart,
                TotalPrice = cart.Sum(item => item.TotalPrice)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = productsV.FirstOrDefault(p => p.Id == productId);
            if (product != null && product.Quantity > 0)
            {
                var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
                if (cartItem == null)
                {
                    cart.Add(new CartItem
                    {
                        ProductId = productId,
                        ProductName = product.Name,
                        Quantity = 1,
                        Price = product.Price,
                        TotalPrice = product.Price
                    });
                }
                else
                {
                    cartItem.Quantity++;
                    cartItem.TotalPrice = cartItem.Quantity * cartItem.Price;
                }

                product.Quantity--;
            }
            return RedirectToAction("Venta");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            cart.Clear();
            return RedirectToAction("Venta");
        }

        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}






