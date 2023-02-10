using kampong_goods.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Staff
{
    public class StaffLogoutModel : PageModel
    {


            private readonly SignInManager<AppUser> signInManager;
            public StaffLogoutModel(SignInManager<AppUser> signInManager)
            {
                this.signInManager = signInManager;
            }
            public void OnGet()
            {
            }
            public async Task<IActionResult> OnPostStaffLogoutAsync()
            {
                await signInManager.SignOutAsync();
            await HttpContext.SignOutAsync("AdminAuth");

            /*            HttpContext.Response.Cookies.Delete("AdminAuth");
            */// Clear the existing external cookie
            /*await HttpContext.SignOutAsync(
           CookieAuthenticationDefaults.AuthenticationScheme);*/
            return RedirectToPage("/Staff/StaffLogin");
            }
            public async Task<IActionResult> OnPostStaffDontLogoutAsync()
            {
                return RedirectToPage("StaffProfile");
            }
        }
    }
