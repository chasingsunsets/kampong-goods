using kampong_goods.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace kampong_goods.Pages.Staff
{
    [Authorize(Roles = "Staff")]

    public class ChangePWSModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;


        [BindProperty]
        public ChangePassword ChModel { get; set; }

        public ChangePWSModel(UserManager<AppUser> userManager,
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
                return RedirectToPage("Profile");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByIdAsync(ChModel.Id);

                if (user != null)
                {
                    System.Diagnostics.Debug.WriteLine(ChModel.Code);
                    var result = await userManager.ResetPasswordAsync(user, ChModel.Code, ChModel.Password);
                    if (result.Succeeded)
                    {

                        await signInManager.SignOutAsync();
                        await HttpContext.SignOutAsync("AdminAuth");
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Password changed successfully, you can now try logging in.");
                        return RedirectToPage("StaffLogin");
                    }





                }

                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("No user found.");
            }
            return Page();
        }


    }
}
