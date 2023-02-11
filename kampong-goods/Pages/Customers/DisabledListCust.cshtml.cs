using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Customers
{
        [Authorize(Roles = "Staff")]
        public class DisabledListCustModel : PageModel
        {
            private readonly CustomerService _customerService;
            private readonly StaffService _staffService;

            private UserManager<AppUser> userManager { get; }
            private SignInManager<AppUser> signInManager { get; }

            private readonly RoleManager<IdentityRole> roleManager;

            public DisabledListCustModel(CustomerService customerService, StaffService staffService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
            {
                _customerService = customerService;
                _staffService = staffService;
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.roleManager = roleManager;


            }
            public void OnGet()
            {
            }

            public async Task<IActionResult> OnGetCustomerEnable(string id)
            {
                if (id == null)
                {
                    return Page();
                }


                var user = _customerService.GetCustomerById(id);
                System.Diagnostics.Debug.WriteLine("await" + user);

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                Debug.WriteLine("current" + userid);


                if (userid == id)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Cannot enable own account, require other staff account to perform such action for you.");
                    return Page();
                }

            if (user != null)
            {
                bool isDisabledCust = await userManager.IsInRoleAsync(user, "CustomerDisabled");
                if (isDisabledCust)
                {


                    var removefromrole = await userManager.RemoveFromRoleAsync(user, "CustomerDisabled");
                    if (removefromrole.Succeeded)
                    {
                        //Add to cust role
                        var enable = await userManager.AddToRoleAsync(user, "Customer");
                        if (enable.Succeeded)
                        {
                            TempData["FlashMessage.Type"] = "success";
                            TempData["FlashMessage.Text"] = string.Format("Account {0} enabled successfully.", user.UserName);
                            return RedirectToPage();
                        }
                        foreach (var error in enable.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                    foreach (var error in removefromrole.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
            }


                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("No such customer account disabled.");
                return RedirectToPage();
            }


        }
    }