using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace kampong_goods.Pages.Customers
{
    public class ForgotPasswordModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IMailService mailService;

        [BindProperty]
        public ForgotPassword FModel { get; set; }



        public ForgotPasswordModel(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IMailService mailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mailService = mailService;

        }
        public void OnGet()
        {
        }

             public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(FModel.Email);

                if (user != null)
                {
                    var code=await userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackurl = Url.PageLink("ResetPassword", null, new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                        
/*                        Url.Action("ResetPassword", "Customers", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
*/                    System.Diagnostics.Debug.WriteLine(callbackurl);

                    await mailService.SendEmailAsync(FModel.Email, "Reset Password Confirmation", "Please reset your email " +
                        "<a href=\"" + callbackurl + "\">here</a>");

                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Please check your email to reset your password.");
                    return Page();


                }

                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("No account with this email has registered with us.");
            }
            return Page();
        }
    }
}
