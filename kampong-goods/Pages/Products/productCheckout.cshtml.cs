using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Pages.Products
{
    public class productCheckoutModel : PageModel
    {
        private readonly CheckoutService _checkoutService;
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomerService _customerService;

        public productCheckoutModel(CheckoutService checkoutService, ProductService productService, UserManager<AppUser> userManager, CustomerService customerService)
        {
            _checkoutService = checkoutService;
            _productService = productService;
            _userManager = userManager;
            _customerService = customerService;
        }

        [BindProperty]
        public Checkout MyCheckout { get; set; } = new();
        public string productName { get; set; }
        public string productImage { get; set; }
        public string productSeller { get; set; }
        public string productCost { get; set; }

        public List<Product> ProductList { get; set; } = new();

        public List<AppUser> CustomerList { get; set; } = new();

        public AppUser user { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            Product? product = _productService.GetProductById(id);
            if (product != null)
            {
                MyCheckout.ProductId = product.ProductId;
                productName = product.ProductName;
                productImage = product.ImageURL;
                productSeller = product.UserId;
                productCost = product.Price.ToString();
                CustomerList = _customerService.GetAll();
                return Page();
            }

            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format(
                "Product ID {0} not found", id);
                return Redirect("/Products/productListing");
            }
        }

        public async Task<IActionResult> OnPost(string id)
        {
            if (ModelState.IsValid)
            {
                //gets the logged in user
                var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
                user = await _userManager.Users.FirstAsync(u => u.Id == userId);

                Product? product = _productService.GetProductById(id);

                MyCheckout.ProductId = product.ProductId;
                MyCheckout.UserId = user.Id;
                product.Status = "Sold";
                MyCheckout.OrderStatus = "Ordered";

                Checkout? checkout = _checkoutService.GetCheckoutById(MyCheckout.CheckoutId);
                if (checkout != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format(
                    "Checkout ID {0} alreay exists", MyCheckout.CheckoutId);
                    return Page();
                }

                _checkoutService.AddCheckout(MyCheckout);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format(
                "Checkout {0} is added", MyCheckout.CheckoutId);
                return Redirect("/Products/productCatalogue");
            }
            return Page();
        }
    }
}
