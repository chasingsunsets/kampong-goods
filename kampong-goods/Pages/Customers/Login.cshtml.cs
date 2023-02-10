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
        public LoginModel(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.UserName, LModel.Password,
                LModel.RememberMe, false);
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

                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Username or Password incorrect");
            }
            return Page();
        }
    }
}
