using farmacius.Models;
using farmacius.Logic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using farmacius.DataBase;
using SQLitePCL;
using System.Linq;

namespace farmacius.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FarmaciusContext _context;
        private static List<Product> products = new List<Product>();
        private static List<CartItem> cart = new List<CartItem>();

        private UserLogic userLogic = new UserLogic();

        public HomeController(ILogger<HomeController> logger, FarmaciusContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    ProductList productList = new ProductList();

                    productList.Products = _context.Products
                        .Select(p => new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description != null ? p.Description : string.Empty, 
                            Price = p.Price,
                            Quantity = p.Quantity,
                            ImageUrl = p.ImageUrl != null ? p.ImageUrl : string.Empty 
                        }).ToList();

                    return View("Index", productList);

                }
                else
                {
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult ProductList(Login model)
        {
            ProductList productList = new ProductList();

            productList.Products = _context.Products
                        .Select(p => new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description != null ? p.Description : string.Empty,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            ImageUrl = p.ImageUrl != null ? p.ImageUrl : string.Empty
                        }).ToList();

            return View("Index", productList);
        }

        [HttpGet]
        public IActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {

            _context.Products.Add(model);
            _context.SaveChanges();

            ProductList productList = new ProductList();

            productList.Products = _context.Products
                        .Select(p => new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description != null ? p.Description : string.Empty,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            ImageUrl = p.ImageUrl != null ? p.ImageUrl : string.Empty
                        }).ToList();

            return View("Index", productList);
        }

        #region Venta

        [HttpGet]
        public IActionResult Venta()
        {
            products = _context.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description != null ? p.Description : string.Empty,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageUrl = p.ImageUrl != null ? p.ImageUrl : string.Empty
            }).ToList();

            var model = new SaleViewModel
            {
                vProducts = products,
                Cart = cart,
                TotalPrice = cart.Sum(item => item.TotalPrice)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
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