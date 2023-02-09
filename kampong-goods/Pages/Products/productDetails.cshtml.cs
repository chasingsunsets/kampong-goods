using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Pages.Products
{
    public class productDetailsModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;
        private readonly ConditionService _conditionService;
        private readonly CategoryService _categoryService;

        public productDetailsModel(ProductService productService, CustomerService customerService, ConditionService conditionService, CategoryService categoryService)
        {
            _productService = productService;
            _customerService = customerService;
            _conditionService = conditionService;
            _categoryService = categoryService;
        }

        public List<Product> ProductList { get; set; } = new();

        public List<AppUser> CustomerList { get; set; } = new();

        public List<Condition> ConditionList { get; set; } = new();

        public List<Category> CategoryList { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            Product? product = _productService.GetProductById(id);
            if (product != null)
            {
                CustomerList = _customerService.GetAll();
                ConditionList = _conditionService.GetAll();
                CategoryList = _categoryService.GetAll();
                ProductList.Add(product);
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
    }
}
