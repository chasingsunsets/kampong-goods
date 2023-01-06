using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Pages.Customers
{
    public class ProfileModel : PageModel

    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }
        private readonly CustomerService _customerService;

        public ProfileModel(CustomerService customerService)
        {
            _customerService = customerService;
        }


        public List<AppUser> CustProfile { get; set; } = new();



        public async Task<IActionResult> OnGet(string id)
        {
            if (id == null)
            {
                return Page();
            }


            /*            var user = await userManager.GetUserAsync(HttpContext.User);
            */
            /*            var user = _customerService.GetCustomerById(id);
                        System.Diagnostics.Debug.WriteLine("await" + user);*/
            var user = await userManager.FindByIdAsync(id);
            System.Diagnostics.Debug.WriteLine("await" + user); 
            if (user != null)
            {
/*                if (user.Id != id)
                {

                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Invalid access, you can only delete the account you logged in with.");
                    return Page();
                }*/

/*                else {
*/                   var result= await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                 

                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account deleted.");
                        return RedirectToPage("Index");
                    }
                /*}*/
                
            }

            return RedirectToPage();
        }



    }
}


