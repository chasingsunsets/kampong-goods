using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace kampong_goods.Pages.Products
{
    public class CatalogueModel : PageModel
    {
        private readonly ProductService _productService;

        public CatalogueModel(ProductService productService)
        {
            _productService = productService;
        }

        public List<Product> ProductList { get; set; } = new();

        public void OnGet()
        {
            ProductList = _productService.GetAll();
        }
    }
}
