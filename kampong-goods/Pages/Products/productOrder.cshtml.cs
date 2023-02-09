using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Products
{
    public class productOrderModel : PageModel
    {
        private readonly CheckoutService _checkoutService;
        private readonly ProductService _productService;

        public productOrderModel(CheckoutService checkoutService, ProductService productService)
        {
            _checkoutService = checkoutService;
            _productService = productService;
        }

        [BindProperty]
        public Checkout MyCheckout { get; set; } = new();
        public string productName { get; set; }
        public string productImage { get; set; }
        public string productSeller { get; set; }
        public string productCost { get; set; }

        public List<Product> ProductList { get; set; } = new();
        public List<Checkout> CheckoutList { get; set; } = new();

        public void OnGet()
        {
            ProductList = _productService.GetAll();
            CheckoutList = _checkoutService.GetAll();
        }
    }
}
