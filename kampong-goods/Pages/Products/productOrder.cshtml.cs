using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Products
{
    public class productOrderModel : PageModel
    {
        private readonly CheckoutService _checkoutService;
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomerService _customerService;

        //SendGrid
        private IMailService _mailService;

        public productOrderModel(CheckoutService checkoutService, ProductService productService, UserManager<AppUser> userManager, CustomerService customerService, IMailService mailService)
        {
            _checkoutService = checkoutService;
            _productService = productService;
            _userManager = userManager;
            _customerService = customerService;
            _mailService = mailService;
        }

        [BindProperty]
        public Checkout MyCheckout { get; set; } = new();

        public List<Product> ProductList { get; set; } = new();

        public List<Checkout> CheckoutList { get; set; } = new();

        public List<AppUser> CustomerList { get; set; } = new();

        public List<Checkout> NotFulfilled { get; set; } = new();

        public List<Checkout> Fulfilled { get; set; } = new();

        public AppUser user { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //gets the logged in user
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            //gets checkout id
            CheckoutList = _checkoutService.GetAll();

            //only gets the checkout for that specific logged in user
            foreach (var checkouts in CheckoutList)
            {
                ProductList = _productService.GetAll();

                foreach(var product in ProductList)
                {
                    if(checkouts.ProductId == product.ProductId)
                    {
                        // gets the seller id
                        if(product.UserId == userId)
                        {
                            if (checkouts.OrderStatus == "Ordered")
                            {
                                //add inside list
                                NotFulfilled.Add(checkouts);
                            }
                            else if (checkouts.OrderStatus == "Shipped" || checkouts.OrderStatus == "Delivered" || checkouts.OrderStatus == "Received")
                            {
                                //add inside list
                                Fulfilled.Add(checkouts);
                            }
                        }
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnGetStatus(string id)
        {
            Debug.WriteLine("is it working?");
            if (id == null)
            {
                return Page();
            }

            var checkout = _checkoutService.GetCheckoutById(id);
            System.Diagnostics.Debug.WriteLine("changing status" + checkout);

            if (checkout != null)
            {
                checkout.OrderStatus = "Shipped";
                checkout.ShipTime = DateTime.Now;

                //add time to deliveredTime
                var shipTime = checkout.ShipTime;
                checkout.DeliveredTime = shipTime.Value.AddMinutes(1);

                _checkoutService.UpdateCheckout(checkout);
                var content = "<body style=\"background-color: #f1ede5; color: black\">\r\n\t\t<br><br>\r\n\t\t<!-- start preheader -->\r\n\t\t<div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">\r\n\t\t\t" + "Your order is on it's way!" + "\r\n\t\t</div>\r\n\t\t<!-- end preheader -->" +
                    "\r\n\r\n\t\t<!-- start body -->\r\n\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n\r\n\t\t\t<!-- start copy block -->\r\n            <tr>\r\n\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\">\r\n\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\t\t\t\t<!-- start copy -->\r\n\t\t\t\t<tr>\r\n\t\t\t\t\t<td align=\"left\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n\t\t\t\t\t<h2 style=\"margin: 0; text-align:center;\">" + "Your order is on it's way!" + "</h2>\r\n\t\t\t\t\t</td>\r\n\t\t\t\t</tr>\r\n\t\t\t\t<!-- end copy -->" +
                    "\r\n\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t\t</tr>\r\n\r\n\t\t\t<tr>\r\n\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\">\r\n\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\t\t\t\t<!-- start copy -->\r\n\t\t\t\t<tr>\r\n\t\t\t\t\t<td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n\t\t\t\t\t<p style=\"margin: 0;\">Dear " + checkout.FName + checkout.LName + ", </p>\r\n                    <p style=\"margin: 0;\">" +
                    "<br><br>Your order has been shipped by the seller! Look out for it's update soon!" +
                    "</p>\r\n\t\t\t\t\t</td>             \r\n\t\t\t\t</tr>\r\n\r\n                <tr>\r\n\t\t\t\t\t<td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n\t\t\t\t\t<p style=\"margin: 0;\">Warmest Regards, </p>\r\n            " +
                    "        <p style=\"margin: 0;\">Kampong Goods Team</p>\r\n\t\t\t\t\t</td>\r\n\t\t\t\t</tr>\r\n\t\t\t\t<!-- end copy -->\r\n\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t\t</tr>\r\n\t\t\t<!-- end copy block -->\r\n\r\n\t\t\t<!-- start footer -->\r\n\t\t\t<tr>\r\n\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\" style=\"padding: 24px;\">\r\n\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\r\n\t\t\t\t<!-- start permission -->\r\n                <tr>\r\n\t\t\t\t\t<td align=\"center\" bgcolor=\"#f1ede5\" style=\"padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;\">\r\n\t\t\t\t\t<p style=\"margin: 0;\"><a href=\"${unSubURL}\" target=\"_blank\">Unsubscribe Here</a></p>\r\n\t\t\t\t\t</td>\r\n\t\t\t\t</tr>\r\n\t\t\t\t<!-- end permission -->\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t\t</tr>\r\n\t\t\t<!-- end footer -->\r\n\r\n\t\t</table>";
                await _mailService.SendEmailAsync(checkout.Email, "Order has been shipped!", content);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Order has been fulfilled!");
                return RedirectToPage("productOrder");
            }

            return RedirectToPage();
        }
    }
}
