using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace kampong_goods.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class StaffProfileModel : PageModel
    { 
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }
        private readonly StaffService _staffService;

        private readonly CustomerService _customerService;


        public StaffProfileModel(StaffService staffService, CustomerService customerService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            _staffService = staffService;
            this.userManager = userManager;
            this.signInManager = signInManager;



        }


        public List<AppUser> CustProfile { get; set; } = new();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetDeleteS(string id)
        {
            if (id == null)
            {
                return Page();
            }



            /*            var test = await userManager.FindByIdAsync("84ae787f-18c5-41f9-9bfd-1e85b5d13778");
                        System.Diagnostics.Debug.WriteLine("hard" + test);*/
            var user = _customerService.GetCustomerById(id);
            System.Diagnostics.Debug.WriteLine("await" + user);



            /*            var user = await userManager.FindByIdAsync(username.Id);
            */

            if (user != null)
            {
                if (user.Id != id)
                {

                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Invalid access, you can only delete the account you logged in with.");
                    return Page();
                }

                else
                {
                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        await signInManager.SignOutAsync();
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account deleted.");
                        return RedirectToPage("/Staff/StaffLogin");
                    }
                }

            }

            return RedirectToPage();
        }


        public async Task<IActionResult> OnGetChangePWS(string id)
        {
            if (id == null)
            {
                return Page();
            }

            var user = _customerService.GetCustomerById(id);
            System.Diagnostics.Debug.WriteLine("await" + user);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user != null)
            {
                if (userid != id)
                {

                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Invalid access, you can only change password of the account you logged in with.");
                    return Page();
                }

                else
                {

                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackurl = Url.PageLink("ChangePWS", null, new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    System.Diagnostics.Debug.WriteLine(callbackurl);
                    return Redirect(callbackurl);


                }
            }
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("No user found.");

            return RedirectToPage();
        }


    }
}








