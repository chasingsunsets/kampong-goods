using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using SendGrid.Helpers.Mail;

namespace kampong_goods.Pages.Products
{
    [Authorize]
    public class productCheckoutModel : PageModel
    {
        private readonly CheckoutService _checkoutService;
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomerService _customerService;

        //SendGrid
        private IMailService _mailService;

        public productCheckoutModel(CheckoutService checkoutService, ProductService productService, UserManager<AppUser> userManager, CustomerService customerService, IMailService mailService)
        {
            _checkoutService = checkoutService;
            _productService = productService;
            _userManager = userManager;
            _customerService = customerService;
            _mailService = mailService;
        }

        [BindProperty]
        public Checkout MyCheckout { get; set; } = new();
        public string productName { get; set; }
        public string productImage { get; set; }
        public string productSeller { get; set; }
        public string productCost { get; set; }

        public List<Product> ProductList { get; set; } = new();

        public List<AppUser> CustomerList { get; set; } = new();

        public AppUser user { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            Product? product = _productService.GetProductById(id);
            if (product != null)
            {
                MyCheckout.ProductId = product.ProductId;
                productName = product.ProductName;
                productImage = product.ImageURL;
                productSeller = product.UserId;
                productCost = product.Price.ToString();
                CustomerList = _customerService.GetAll();
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
        
        public async Task<IActionResult> OnPost(string id)
        {
            if (ModelState.IsValid)
            {
                //gets the logged in user
                var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
                user = await _userManager.Users.FirstAsync(u => u.Id == userId);

                Product? product = _productService.GetProductById(id);

                MyCheckout.ProductId = product.ProductId;
                MyCheckout.UserId = user.Id;
                product.Status = "Sold";

                Checkout? checkout = _checkoutService.GetCheckoutById(MyCheckout.CheckoutId);
                if (checkout != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format(
                    "Checkout ID {0} alreay exists", MyCheckout.CheckoutId);
                    return Page();
                }

                _checkoutService.AddCheckout(MyCheckout);
                var content = "<body style=\"background-color: #f1ede5; color: black\">\r\n\t\t<br><br>\r\n\t\t<!-- start preheader -->\r\n\t\t<div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">\r\n\t\t\t" + "Thank you for your purchase!" + "\r\n\t\t</div>\r\n\t\t<!-- end preheader -->" +
                    "\r\n\r\n\t\t<!-- start body -->\r\n\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n\r\n\t\t\t<!-- start copy block -->\r\n            <tr>\r\n\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\">\r\n\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\t\t\t\t<!-- start copy -->\r\n\t\t\t\t<tr>\r\n\t\t\t\t\t<td align=\"left\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n\t\t\t\t\t<h2 style=\"margin: 0; text-align:center;\">" + "Thank you for your purchase!" + "</h2>\r\n\t\t\t\t\t</td>\r\n\t\t\t\t</tr>\r\n\t\t\t\t<!-- end copy -->" +
                    "\r\n\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t\t</tr>\r\n\r\n\t\t\t<tr>\r\n\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\">\r\n\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\t\t\t\t<!-- start copy -->\r\n\t\t\t\t<tr>\r\n\t\t\t\t\t<td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n\t\t\t\t\t<p style=\"margin: 0;\">Dear " + MyCheckout.FName + MyCheckout.LName + ", </p>\r\n                    <p style=\"margin: 0;\">" +
                    "<br><br>Your order has been placed sucessfully! Look out for it's update soon!" +
                    "</p>\r\n\t\t\t\t\t</td>             \r\n\t\t\t\t</tr>\r\n\r\n                <tr>\r\n\t\t\t\t\t<td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n\t\t\t\t\t<p style=\"margin: 0;\">Warmest Regards, </p>\r\n            " +
                    "        <p style=\"margin: 0;\">Kampong Goods Team</p>\r\n\t\t\t\t\t</td>\r\n\t\t\t\t</tr>\r\n\t\t\t\t<!-- end copy -->\r\n\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t\t</tr>\r\n\t\t\t<!-- end copy block -->\r\n\r\n\t\t\t<!-- start footer -->\r\n\t\t\t<tr>\r\n\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\" style=\"padding: 24px;\">\r\n\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\r\n\t\t\t\t<!-- start permission -->\r\n                <tr>\r\n\t\t\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\" style=\"padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;\">\r\n\t\t\t\t\t<p style=\"margin: 0;\"><a href=\"${unSubURL}\" target=\"_blank\">Unsubscribe Here</a></p>\r\n\t\t\t\t\t</td>\r\n\t\t\t\t</tr>\r\n\t\t\t\t<!-- end permission -->\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t\t</tr>\r\n\t\t\t<!-- end footer -->\r\n\r\n\t\t</table>";
                
                await _mailService.SendEmailAsync(MyCheckout.Email, "Order has been placed!", content); 
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Order has been placed succesfully!", MyCheckout.CheckoutId);
                return Redirect("/Products/productPurchase");
            }
            return Page();
        }
    }
}
