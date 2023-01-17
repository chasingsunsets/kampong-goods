using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;

namespace kampong_goods.Pages.Products
{
    public class productDetailsModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ConditionService _conditionService;
        private IWebHostEnvironment _environment;

        public productDetailsModel(ProductService productService,
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

        public IActionResult OnGet(string id)
        {
            Product? product = _productService.GetProductById(id);
            if (product != null)
            {
                MyProduct = product;
                //MyProduct.Condition = _conditionService.GetConditionById(product.ConditionId);
                //MyProduct.Category = _categoryService.GetCategoryById(product.CategoryId);
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
                if(Upload != null)
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

                _productService.UpdateProduct(MyProduct);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Product {0} is updated", MyProduct.ProductName);
            }
            return Page();
        }
    }
}
