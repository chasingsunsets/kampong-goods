using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Pages.Products
{

    public class CatalogueModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomerService _customerService;

        public CatalogueModel(ProductService productService, UserManager<AppUser> userManager, CustomerService customerService)
        {
            _productService = productService;
            _userManager = userManager;
            _customerService = customerService;
        }

        public List<Product> ProductList { get; set; } = new();

        public List<Product> showProductList { get; set; } = new();

        public AppUser user { get; set; } = new();

        public List<AppUser> CustomerList { get; set; } = new();

        public AppUser anyUser { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //see if got any user
            anyUser = await _userManager.GetUserAsync(HttpContext.User);

            if(anyUser == null)
            {
                //will change to login later
                ProductList = _productService.GetAll();
                CustomerList = _customerService.GetAll();
            }

            //if got user, only show product of other user
            if(anyUser != null)
            {
                var userId = anyUser.Id;
                user = await _userManager.Users.FirstAsync(u => u.Id == userId);

                ProductList = _productService.GetAll();
                CustomerList = _customerService.GetAll();
                foreach (var item in ProductList)
                {
                    if(item.UserId != userId)
                    {
                        showProductList.Add(item);
                    }
                }
            }

            return Page();
        }
    }
}
