using kampong_goods.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace kampong_goods.Pages.Customers
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<AppUser> signInManager;
        private UserManager<AppUser> userManager { get; }
        private readonly RoleManager<IdentityRole> roleManager;

        public LoginModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var useracc = await userManager.FindByNameAsync(LModel.UserName);

                if (useracc != null)
                {


                    

                    bool isDisabledStaff = await userManager.IsInRoleAsync(useracc, "StaffDisabled");
                    bool isDisabledCust = await userManager.IsInRoleAsync(useracc, "CustomerDisabled");

                    if (isDisabledStaff||isDisabledCust)
                    {


                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Account is disabled. Contact Staff for help.");
                        return Page();

                    }

                    if (!await userManager.IsEmailConfirmedAsync(useracc))
                    {
                        /*                        string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");
                        */
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Please check your email and confirm your email before you can log in.");
                        return Page();
                    }

                    var identityResult = await signInManager.PasswordSignInAsync(LModel.UserName, LModel.Password, LModel.RememberMe, false);
                    if (identityResult.Succeeded)
                    {
                        //Create the security context
                        var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, LModel.UserName),
                     };
                        var i = new ClaimsIdentity(claims, "UserAuth");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
                        await HttpContext.SignInAsync("UserAuth", claimsPrincipal);


                        /*                    var userId = signInManager.UserManager.Users.FirstOrDefault()?.Id; 
                        */
                        return RedirectToPage("Profile");
                    }





                }

                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Username or Password incorrect");
            }
            return Page();
        }
    }
}
