using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace kampong_goods.Pages.Staff
{
    public class StaffLoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }
        private UserManager<AppUser> userManager { get; }
        private readonly RoleManager<IdentityRole> roleManager;


        private readonly SignInManager<AppUser> signInManager;
        private readonly StaffService _staffService;

        public StaffLoginModel(StaffService staffService, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _staffService = staffService;
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


            var staff = await userManager.FindByNameAsync("mainstaff");
            if (staff==null)
            {
                var user = new AppUser()
                {
                    
                    UserName = "mainstaff",
                    Email = "kamponggoods@gmail.com",

                    LName = "Main",
                    FName = "Staff",

                    PhoneNumber = "98554466",
                    Address = "BLK 22 QUEENSTOWN 219288",

                };


                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    //Add users to Admin Role
                    result = await userManager.AddToRoleAsync(user, "Staff");

                    var createstaff = new StaffInfo()
                    {
                        NRIC = "T0498998Z",
                        User = user
                    };
                    //Add users to db
                    _staffService.AddStaff(createstaff);

                }

            }
            return Page();

        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var useracc = await userManager.FindByNameAsync(LModel.UserName);

                if (useracc!=null)
                {

                    bool isDisabledStaff = await userManager.IsInRoleAsync(useracc, "StaffDisabled");
                    if (isDisabledStaff)
                    {


                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Account is disabled. Contact Staff for help.");
                        return Page();

                    }

                    bool isStaff = await userManager.IsInRoleAsync(useracc, "Staff");
                    if (isStaff)
                    {


                        var identityResult = await signInManager.PasswordSignInAsync(LModel.UserName, LModel.Password, LModel.RememberMe, false);
                        if (identityResult.Succeeded)
                        {
                            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, LModel.UserName),};
                            var i = new ClaimsIdentity(claims, "AdminAuth");
                            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
                            await HttpContext.SignInAsync("AdminAuth", claimsPrincipal);

                            return RedirectToPage("Dashboard");

                        }
                      
 
                     }

       

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
