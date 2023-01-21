using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace kampong_goods.Pages.Products
{
    public class AddproductModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ConditionService _conditionService;
        private IWebHostEnvironment _environment;

        public AddproductModel(ProductService productService,
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

        public void OnGet()
        {
            CategoryList = _categoryService.GetAll();
            ConditionList = _conditionService.GetAll();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                Guid myuuid = Guid.NewGuid();
                MyProduct.ProductId = myuuid.ToString();

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                Debug.WriteLine(userid);
                MyProduct.UserId = userid;

                //check id
                Product? product = _productService.GetProductById(MyProduct.ProductId);
                if(product != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Product ID {0} already exists", MyProduct.ProductId);
                    return Page();
                }

                if(Upload != null)
                {
                    if(Upload.Length > 2 * 1024 * 1024)
                    {
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("File size cannot exceed 2MB");
                        //ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyProduct.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                _productService.AddProduct(MyProduct);
                TempData["FlashMessage.Type"] = "Success";
                TempData["FlashMessage.Text"] = string.Format("Product {0} is added", MyProduct.ProductName);
                return Redirect("/Products/productListing");
            }
            return Page();
        }
    }
}
