using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace kampong_goods.Pages.Staff
{
    [Authorize(Roles ="Staff")]
    public class StaffListModel : PageModel
    {
        private readonly CustomerService _customerService;
        private UserManager<AppUser> userManager { get; }

        public StaffListModel(CustomerService customerService, UserManager<AppUser> userManager)
        {
            _customerService = customerService;
            this.userManager = userManager;

        }
        public List<AppUser> CustList { get; set; } = new();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDelete(string id)
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
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account deleted.");
                        return RedirectToPage("StaffList");
                    }
                }

            }

            return RedirectToPage();
        }
    }
}
