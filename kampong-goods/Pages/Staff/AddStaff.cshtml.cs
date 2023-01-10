using kampong_goods.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace kampong_goods.Pages.Staff
{
    [Authorize(Roles = "Staff")]

    public class AddStaffModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public AddStaff AModel { get; set; }



        public AddStaffModel(UserManager<AppUser> userManager,
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
                    NRIC=AModel.NRIC,
                    UserName = AModel.Username,
                   /* Email = RModel.Email,*/

                    LName = AModel.LName,
                    FName = AModel.FName,

                    PhoneNumber = AModel.PhoneNo,
/*                    Address = RModel.Address,
*/
                };

                //Create the Customer role if NOT exist
                IdentityRole role = await roleManager.FindByNameAsync("Staff");
                if (role == null)
                {
                    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Staff"));
                    if (!result2.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role staff failed");
                        /*         TempData["FlashMessage.Type"] = "danger";
                                 TempData["FlashMessage.Text"] = string.Format("Account registration fail.");*/
                    }
                }


                    var result = await userManager.CreateAsync(user, AModel.Password);
                    if (result.Succeeded)
                    {
                        //Add users to Admin Role
                        result = await userManager.AddToRoleAsync(user, "Staff");

                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account {0} successfully registered.", AModel.Username);
                        await signInManager.SignInAsync(user, false);
                        return RedirectToPage("Dashboard");
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
