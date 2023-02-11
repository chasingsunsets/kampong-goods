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
        private readonly ConditionService _conditionService;
        private readonly CategoryService _categoryService;

        public CatalogueModel(ProductService productService, UserManager<AppUser> userManager, CustomerService customerService, ConditionService conditionService, CategoryService categoryService)
        {
            _productService = productService;
            _userManager = userManager;
            _customerService = customerService;
            _conditionService = conditionService;
            _categoryService = categoryService;
        }

        public List<Product> AllProductList { get; set; } = new();

        public List<Product> ProductList { get; set; } = new();

        public List<Product> showProductList { get; set; } = new();

        public AppUser user { get; set; } = new();

        public List<AppUser> CustomerList { get; set; } = new();

        public AppUser anyUser { get; set; } = new();

        public List<Condition> ConditionList { get; set; } = new();

        public List<Category> CategoryList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //see if got any user logged in
            anyUser = await _userManager.GetUserAsync(HttpContext.User);

            if(anyUser == null)
            {
                //will change to login later
                AllProductList = _productService.GetAll();
                foreach(var product in AllProductList)
                {
                    if(product.Status == "Available" || product.Status == "Reserved")
                    {
                        ProductList.Add(product);
                    }
                }
                CustomerList = _customerService.GetAll();
                ConditionList = _conditionService.GetAll();
                CategoryList = _categoryService.GetAll();
            }

            //if got user, only show product of other user
            if(anyUser != null)
            {
                var userId = anyUser.Id;
                user = await _userManager.Users.FirstAsync(u => u.Id == userId);

                AllProductList = _productService.GetAll();
                foreach (var product in AllProductList)
                {
                    if (product.Status == "Available" || product.Status == "Reserved")
                    {
                        ProductList.Add(product);
                    }
                }
                CustomerList = _customerService.GetAll();
                ConditionList = _conditionService.GetAll();
                CategoryList = _categoryService.GetAll();
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
