using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Customers
{
    public class EditProfileModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly CustomerService _customerService;


        public EditProfileModel(CustomerService customerService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            this.userManager = userManager;
            this.signInManager = signInManager;


        }

        [BindProperty]
        public AppUser CustProfile { get; set; } = new();
        public IActionResult OnGet(string id)
        {
/*                        System.Diagnostics.Debug.WriteLine(id);
*/          AppUser? customer = _customerService.GetCustomerById(id);


            if (customer != null)
            {
                CustProfile = customer;
                


                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Invalid access");
                return Redirect("/Customer");
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {

/*                System.Diagnostics.Debug.WriteLine("eh" + CustProfile.Id);
*/
               /* AppUser ser = await userManager.GetUserAsync(HttpContext.User);
                System.Diagnostics.Debug.WriteLine("context" + ser.Id);*/

                var user = await userManager.FindByIdAsync(CustProfile.Id);

                user.Id = CustProfile.Id;
                user.UserName = CustProfile.UserName;
                user.Email = CustProfile.Email;
                user.LName = CustProfile.LName;
                user.FName = CustProfile.FName;
                user.PhoneNumber = CustProfile.PhoneNumber;
                user.Address = CustProfile.Address;


                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Profile {0} edited successfully.", CustProfile.UserName);
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();



        }



    }
}
