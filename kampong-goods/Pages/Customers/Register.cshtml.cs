using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;

namespace kampong_goods.Pages.Customers
{
    public class RegisterModel : PageModel
    {

        private UserManager<CustomersInfo> userManager { get; }
        private SignInManager<CustomersInfo> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }



        public RegisterModel(UserManager<CustomersInfo> userManager,
        SignInManager<CustomersInfo> signInManager,
        IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public void OnGet()
        {
        }
        //Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var user = new CustomersInfo()
                {
                    UserName = RModel.Username,
                    Email = RModel.Email,

                    LName = RModel.LName,
                    FName = RModel.FName,

                    PhoneNumber = RModel.PhoneNo,
                    Address = RModel.Address,
                   

                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
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
