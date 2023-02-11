using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Customers
{
    public class ResetPasswordModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;


        [BindProperty]
        public ResetPassword ReModel { get; set; }

        public ResetPasswordModel(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

        }
        public IActionResult OnGet(string code)
        {
            if (code == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Error occurred while processing your request.");
                return RedirectToPage("ForgotPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(ReModel.Email);

                if (user != null)
                {
                    System.Diagnostics.Debug.WriteLine(ReModel.Code);
                    var result = await userManager.ResetPasswordAsync(user, ReModel.Code, ReModel.Password);
                    if (result.Succeeded)
                    {
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Password reset was successfully, you can now try logging in.");
                        return RedirectToPage("Login");
                    }

                    else
                    {
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Current reset password link is not for this account");
                        return Page();

                    }



                }

                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("No account with this email has registered with us.");
            }
            return Page();
        }


    }
}
