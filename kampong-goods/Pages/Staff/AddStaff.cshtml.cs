using kampong_goods.Models;
using kampong_goods.Services;
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

        private readonly StaffService _staffService;
        
        private readonly IMailService mailService;



        [BindProperty]
        public AddStaff AModel { get; set; }



        public AddStaffModel(StaffService staffService, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IMailService mailService)
        {
                _staffService = staffService;
                this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mailService = mailService;


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
/*                    NRIC=AModel.NRIC,
*/                    UserName = AModel.Username,
                    Email = AModel.Email,

                    LName = AModel.LName,
                    FName = AModel.FName,

                    PhoneNumber = AModel.PhoneNo,
                    Address = AModel.Address,

                };

                //Create the Staff role if NOT exist
                IdentityRole role = await roleManager.FindByNameAsync("Staff");
                if (role == null)
                {   
                    IdentityResult result1 = await roleManager.CreateAsync(new IdentityRole("Staff"));
                    if (!result1.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role staff failed");
                 
                    }
                }

                //Create the Customer role if NOT exist
                IdentityRole role2 = await roleManager.FindByNameAsync("Customer");
                if (role2 == null)
                {
                    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Customer"));
                    if (!result2.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role customer failed");

                    }
                }

                var useremail = await userManager.FindByEmailAsync(user.Email);
                if (useremail == null) /*if email not used*/
                {

                    StaffInfo? hvstaff = _staffService.GetStaffbyIC(AModel.NRIC);
                    if (hvstaff == null)
                    {
                            var createuser = await userManager.CreateAsync(user, AModel.Password);
                            if (createuser.Succeeded)
                            {

                                var staff = new StaffInfo()
                                {

                                    NRIC = AModel.NRIC,
                                    User = user

                                };

                                //Add users to Admin Role
/*                                createuser = await userManager.AddToRoleAsync(user, "Customer");
*/                                await userManager.AddToRoleAsync(user, "Staff");
                                _staffService.AddStaff(staff);


                            string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackurl = Url.PageLink("ConfirmStaffEmail", null, new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                            System.Diagnostics.Debug.WriteLine(callbackurl);
/*                            await userManager.SetEmailAsync()
*/                            await mailService.SendEmailAsync(user.Email, "Confirm your account", "Please confirm your account by clicking " +
                                                        "<a href=\"" + callbackurl + "\">here</a>");

                            TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("Account {0} added, tell staff to check email and confirm account before logging in.", AModel.Username);
/*                                await signInManager.SignInAsync(user, false); ///???
*/                                return RedirectToPage("StaffList");
                            }

                            foreach (var error in createuser.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }

                    }
                    else
                    {
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Text"] = string.Format("Account with NRIC: {0} is registered already.", AModel.NRIC);
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
