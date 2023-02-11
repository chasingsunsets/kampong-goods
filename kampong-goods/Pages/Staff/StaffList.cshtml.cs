using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Staff
{
    [Authorize(Roles ="Staff")]
    public class StaffListModel : PageModel
    {
        private readonly CustomerService _customerService;
        private readonly StaffService _staffService;

        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        public StaffListModel(CustomerService customerService, StaffService staffService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _customerService = customerService;
            _staffService = staffService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;


        }
        public List<AppUser> CustList { get; set; } = new();
        public List<StaffInfo> IClist { get; set; } = new();
        public void OnGet()
        {
            IClist=_staffService.GetAll();
        }

        public async Task<IActionResult> OnGetStaffDisable(string id)
        {
            if (id == null)
            {
                return Page();
            }



            /*            var test = await userManager.FindByIdAsync("84ae787f-18c5-41f9-9bfd-1e85b5d13778");
                        System.Diagnostics.Debug.WriteLine("hard" + test);*/
            var user = _customerService.GetCustomerById(id);
            System.Diagnostics.Debug.WriteLine("await" + user);

            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            Debug.WriteLine("current"+userid);

/*            var stafflist = await userManager.GetUsersInRoleAsync("Staff");
*/

            if (userid == id)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Cannot disable own account, require other staff account to perform such action for you.");
                return Page();
            }

            if (user.UserName == "mainstaff")
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Cannot disable mainstaff account as it is the main account.");
                return Page();
            }


            /*            var user = await userManager.FindByIdAsync(username.Id);
            */
            bool isStaff = await userManager.IsInRoleAsync(user, "Staff");
            if (user != null && isStaff)
            {
                //Create the Disable Staff role if NOT exist
                IdentityRole role = await roleManager.FindByNameAsync("StaffDisabled");
                if (role == null)
                {
                    IdentityResult result1 = await roleManager.CreateAsync(new IdentityRole("StaffDisabled"));
                    if (!result1.Succeeded)
                    {
                        ModelState.AddModelError("", "Disable staff role failed");

                    }
                }

                var removefromrole=await userManager.RemoveFromRoleAsync(user, "Staff");
                if (removefromrole.Succeeded)
                {
                    //Add to disabled role
                    var disable=await userManager.AddToRoleAsync(user, "StaffDisabled");
                    if (disable.Succeeded)
                    {
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account {0} disabled successfully.", user.UserName);
                        return RedirectToPage("StaffList");
                    }
                    foreach (var error in disable.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                foreach (var error in removefromrole.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }


            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("No such staff account.");
            return RedirectToPage();
        }

        /* public async Task<IActionResult> OnGetDelete(string id)
         {
             if (id == null)
             {
                 return Page();
             }



             *//*            var test = await userManager.FindByIdAsync("84ae787f-18c5-41f9-9bfd-1e85b5d13778");
                         System.Diagnostics.Debug.WriteLine("hard" + test);*//*
             var user = _customerService.GetCustomerById(id);
             System.Diagnostics.Debug.WriteLine("await" + user);



             *//*            var user = await userManager.FindByIdAsync(username.Id);
             *//*

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
                         TempData["FlashMessage.Type"] = "success";
                         TempData["FlashMessage.Text"] = string.Format("Account deleted.");
                         return RedirectToPage("StaffList");
                     }
                 }

             }

             return RedirectToPage();
         }*/
    }
}
