using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Diagnostics;

namespace kampong_goods.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        public List<Product> ProductList { get; set; } = new();

        public void OnGet()
        {
            ProductList = _productService.GetAll();
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
                System.Diagnostics.Debug.WriteLine("await" + product);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Product deleted.");
                return RedirectToPage("productListing");
            }

            return RedirectToPage();
        }
    }
}
