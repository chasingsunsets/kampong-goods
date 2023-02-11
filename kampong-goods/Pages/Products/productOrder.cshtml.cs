using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace kampong_goods.Pages.Products
{
    public class productOrderModel : PageModel
    {
        private readonly CheckoutService _checkoutService;
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomerService _customerService;

        public productOrderModel(CheckoutService checkoutService, ProductService productService, UserManager<AppUser> userManager, CustomerService customerService)
        {
            _checkoutService = checkoutService;
            _productService = productService;
            _userManager = userManager;
            _customerService = customerService;
        }

        [BindProperty]
        public Checkout MyCheckout { get; set; } = new();

        public List<Product> ProductList { get; set; } = new();

        public List<Checkout> CheckoutList { get; set; } = new();

        public List<AppUser> CustomerList { get; set; } = new();

        public List<Checkout> NotFulfilled { get; set; } = new();

        public List<Checkout> Fulfilled { get; set; } = new();

        public AppUser user { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //gets the logged in user
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            //gets checkout id
            CheckoutList = _checkoutService.GetAll();

            //only gets the checkout for that specific logged in user
            foreach (var checkouts in CheckoutList)
            {
                ProductList = _productService.GetAll();

                foreach(var product in ProductList)
                {
                    if(checkouts.ProductId == product.ProductId)
                    {
                        // gets the seller id
                        if(product.UserId == userId)
                        {
                            if (checkouts.OrderStatus == "Ordered")
                            {
                                //add inside list
                                NotFulfilled.Add(checkouts);
                            }
                            else if (checkouts.OrderStatus == "Shipped")
                            {
                                //add inside list
                                Fulfilled.Add(checkouts);
                            }
                        }
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSetStatus(string id)
        {
            if (id == null)
            {
                return Page();
            }

            var checkout = _checkoutService.GetCheckoutById(id);
            System.Diagnostics.Debug.WriteLine("await" + checkout);

            if (checkout != null)
            {
                checkout.OrderStatus = "Shipped";
                _checkoutService.UpdateCheckout(checkout);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Order has been fulfilled!");
                return RedirectToPage("productOrder");
            }

            return RedirectToPage();
        }
    }
}
