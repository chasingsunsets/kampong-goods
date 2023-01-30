using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Products
{
    public class productCartModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ConditionService _conditionService;
        private IWebHostEnvironment _environment;
        private readonly CartService _cartService;
        private UserManager<AppUser> _userManager { get; }

        public productCartModel(ProductService productService,
            CategoryService categoryService,
            ConditionService conditionService,
            IWebHostEnvironment environment,
            CartService cartService,
            UserManager<AppUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _conditionService = conditionService;
            _environment = environment;
            _cartService = cartService;
            _userManager = userManager;
        }

        [BindProperty]
        public Cart MyCart { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Guid myuuid = Guid.NewGuid();
                MyCart.ProductId = myuuid.ToString();

                var currentUser = await _userManager.GetUserAsync(User);
                MyCart.UserId = currentUser.Id;

                //check id
                Cart? cart = _cartService.GetCartById(MyCart.CartId);
                if (cart != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Product ID {0} already exists", MyCart.CartId);
                    return Page();
                }

                _cartService.AddCart(MyCart);
                TempData["FlashMessage.Type"] = "Success";
                TempData["FlashMessage.Text"] = string.Format("Product {0} is added", MyCart.CartId);
                return Redirect("/Products/productCart");
            }
            return Page();
        }
    }
}
