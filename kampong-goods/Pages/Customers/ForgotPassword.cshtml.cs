using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Customers
{
    public class ForgotPasswordModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public ForgotPassword FModel { get; set; }



        public ForgotPasswordModel(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

        }
        public void OnGet()
        {
        }
    }
}
