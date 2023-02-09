using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Products
{
    public class productEditModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ConditionService _conditionService;
        private IWebHostEnvironment _environment;

        public productEditModel(ProductService productService,
            CategoryService categoryService,
            ConditionService conditionService,
            IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _conditionService = conditionService;
            _environment = environment;
        }

        [BindProperty]
        public Product MyProduct { get; set; } = new();

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public static List<Category> CategoryList { get; set; } = new();
        public static List<Condition> ConditionList { get; set; } = new();
        public IActionResult OnGet(string id)
        {
            CategoryList = _categoryService.GetAll();
            ConditionList = _conditionService.GetAll();

            Product? product = _productService.GetProductById(id);
            if (product != null)
            {
                MyProduct = product;
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

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    if (MyProduct.ImageURL != null)
                    {
                        var oldImageFile = Path.GetFileName(MyProduct.ImageURL);
                        var oldImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, oldImageFile);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyProduct.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                MyProduct.UserId = userid;

                _productService.UpdateProduct(MyProduct);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Product {0} is updated", MyProduct.ProductName);
                return Redirect("/Products/productListing");
            }
            return Page();
        }
    }
}
