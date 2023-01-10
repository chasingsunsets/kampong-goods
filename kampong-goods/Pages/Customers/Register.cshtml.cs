using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;

namespace kampong_goods.Pages.Customers
{
    public class RegisterModel : PageModel
    {

        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public Register RModel { get; set; }



        public RegisterModel(UserManager<AppUser> userManager,
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
        //Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var user = new AppUser()
                {
                    UserName = RModel.Username,
                    Email = RModel.Email,

                    LName = RModel.LName,
                    FName = RModel.FName,

                    PhoneNumber = RModel.PhoneNo,
                    Address = RModel.Address,
                   
                };

                //Create the Customer role if NOT exist
                IdentityRole role = await roleManager.FindByNameAsync("Customer");
                if (role == null)
                {
                    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Customer"));
                    if (!result2.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role customer failed");
               /*         TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Account registration fail.");*/
                    }
                }

                var useremail = await userManager.FindByEmailAsync(user.Email);
                if (useremail == null) /*if email not used*/
                {
                    var result = await userManager.CreateAsync(user, RModel.Password);
                    if (result.Succeeded)
                    {
                        //Add users to Admin Role
                        result = await userManager.AddToRoleAsync(user, "Customer");

                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account {0} successfully registered.", RModel.Username);
                        await signInManager.SignInAsync(user, false);
                        return RedirectToPage("Profile");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Account with email: {0} is registered already.", user.Email);
                }
            }
            return Page();
        }



    }
}
