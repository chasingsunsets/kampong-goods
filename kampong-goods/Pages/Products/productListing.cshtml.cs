using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ConditionService _conditionService;
        private readonly CategoryService _categoryService;

        public IndexModel(ProductService productService, UserManager<AppUser> userManager, ConditionService conditionService, CategoryService categoryService)
        {
            _productService = productService;
            _userManager = userManager;
            _conditionService = conditionService;
            _categoryService = categoryService;
        }

        public List<Product> ProductList { get; set; } = new();

        public List<Product> availableProducts { get; set; } = new();

        public List<Product> reservedProducts { get; set; } = new();

        public List<Product> soldProducts { get; set; } = new();

        public List<Condition> ConditionList { get; set; } = new();

        public List<Category> CategoryList { get; set; } = new();

        public AppUser user { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //gets the logged in user
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            ProductList = _productService.GetAll();
            ConditionList = _conditionService.GetAll();
            CategoryList = _categoryService.GetAll();

            //sorts the products into its availablity
            foreach (var productItem in ProductList)
            {
                if(productItem.UserId == user.Id)
                {
                    if (productItem.Status == "Available")
                    {
                        availableProducts.Add(productItem);
                    }

                    if (productItem.Status == "Reserved")
                    {
                        reservedProducts.Add(productItem);
                    }

                    if (productItem.Status == "Sold")
                    {
                        soldProducts.Add(productItem);
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnGetDelete(string id)
        {
            if(id == null)
            {
                return Page();
            }

            var product = _productService.GetProductById(id);
            System.Diagnostics.Debug.WriteLine("await" + product);

            if(product != null)
            {
                _productService.DeleteProduct(product);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Product = {0} is deleted!", product.ProductName);
                return RedirectToPage("productListing");
            }

            return RedirectToPage();
        }
    }
}
