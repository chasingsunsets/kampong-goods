using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Staff
{
    public class StaffLoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }
        private UserManager<AppUser> userManager { get; }
        private readonly RoleManager<IdentityRole> roleManager;


        private readonly SignInManager<AppUser> signInManager;
  

        public StaffLoginModel(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            IdentityRole role = await roleManager.FindByNameAsync("Staff");
            if (role == null)
            {
                IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Staff"));
                if (!result2.Succeeded)
                {
                    ModelState.AddModelError("", "Create role staff failed");
                    
                }
            }

            var staff = await userManager.FindByNameAsync("specialstaffonly");
            if (staff==null)
            {
                var user = new AppUser()
                {
                    NRIC = "T0498998Z",
                    UserName = "specialstaffonly",
                    /* Email = RModel.Email,*/

                    LName = "Main",
                    FName = "Staff",

                    PhoneNumber = "98554466",
                    /*                    Address = RModel.Address,
                    */
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    //Add users to Admin Role
                    result = await userManager.AddToRoleAsync(user, "Staff");

                }

            }
            return Page();

        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
/*                var userbelong = await userManager.FindByNameAsync(LModel.UserName);
                if (userbelong != null)
                {
                    var isstaff = await userManager.GetRolesAsync("userbelong")*/
                    var identityResult = await signInManager.PasswordSignInAsync(LModel.UserName, LModel.Password,
                LModel.RememberMe, false);
                    if (identityResult.Succeeded)
                    {
                        /*                    var userId = signInManager.UserManager.Users.FirstOrDefault()?.Id; 
                        */
                        return RedirectToPage("Dashboard");
                    }

/*                }
*/
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Username or Password incorrect");
            }
            return Page();
        }
    }
}
