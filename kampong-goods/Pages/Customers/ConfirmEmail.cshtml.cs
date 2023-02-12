using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Customers
{
    public class ConfirmEmailModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        public ConfirmEmailModel(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string userId, string code)
        {
            if (code == null || userId==null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Error occurred while processing your request.");
                return RedirectToPage("Login");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Email confirmed, you can now log into your account.");
                    return RedirectToPage("Login");
                }
            }

            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("User do not exist.");
            return RedirectToPage("Login");
        }

    }
}
