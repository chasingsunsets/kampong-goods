using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Pages.Products
{
    public class productPurchaseModel : PageModel
    {
        private readonly CheckoutService _checkoutService;
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomerService _customerService;

        public productPurchaseModel(CheckoutService checkoutService, ProductService productService, UserManager<AppUser> userManager, CustomerService customerService)
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

        public List<Checkout> NotReceivedList { get; set; } = new();

        public List<Checkout> ReceivedList { get; set; } = new();

        public AppUser user { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //gets the logged in user
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            //gets checkout id
            CheckoutList = _checkoutService.GetAll();

            //only gets the checkout for that specific logged in user
            foreach(var checkouts in CheckoutList)
            {
                //compare with user id
                if(checkouts.UserId == userId)
                {
                    ProductList = _productService.GetAll();

                    if(checkouts.OrderStatus == "Ordered" || checkouts.OrderStatus == "Shipped" || checkouts.OrderStatus == "Delivered")
                    {
                        //add inside list
                        NotReceivedList.Add(checkouts);
                    }
                    else if(checkouts.OrderStatus == "Received")
                    {
                        //add inside list
                        ReceivedList.Add(checkouts);
                    }
                }
            }

            return Page();
        }
    }
}
