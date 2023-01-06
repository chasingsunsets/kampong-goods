using kampong_goods.Models;
using kampong_goods.Pages.Customers;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;

namespace kampong_goods.Pages.Customers
{
    public class EditCustListModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly CustomerService _customerService;


        public EditCustListModel(CustomerService customerService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            this.userManager = userManager;
            this.signInManager = signInManager;


        }

        [BindProperty]
        public AppUser CustAcc { get; set; } = new();




        /*        [BindProperty]
                public Register RModel { get; set; }*/


        public IActionResult OnGet(string id)
        {
/*            System.Diagnostics.Debug.WriteLine("hhhhhh");
            System.Diagnostics.Debug.WriteLine(id);
            System.Diagnostics.Debug.WriteLine("h");*/
            AppUser? customer = _customerService.GetCustomerById(id);
            System.Diagnostics.Debug.WriteLine(id);


            if (customer != null)
            {
                CustAcc = customer;
                System.Diagnostics.Debug.WriteLine(customer.Id);
                System.Diagnostics.Debug.WriteLine("ben" + CustAcc.Id);


                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Customer {0} not found", id);
                return Redirect("/Customer");
            }
        }
        /*public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(CustAcc);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format(
                "Customer {0} is updated", CustAcc.UserName);
            }
            return Page();
        }*/
                
        public async Task<IActionResult> OnPostAsync()
        {
            System.Diagnostics.Debug.WriteLine("eh1" + CustAcc.Id);

            if (ModelState.IsValid)
            {

                System.Diagnostics.Debug.WriteLine("eh" + CustAcc.Id);


/*                var user = new AppUser()
                {
                    Id=CustAcc.Id,
                    UserName = CustAcc.UserName,
                    Email = CustAcc.Email,

                    LName = CustAcc.LName,
                    FName = CustAcc.FName,

                    PhoneNumber = CustAcc.PhoneNumber,
                    Address = CustAcc.Address,


                };
*/

                System.Diagnostics.Debug.WriteLine("fix?" + CustAcc.Id);
                System.Diagnostics.Debug.WriteLine("fix?" + CustAcc.UserName);

                var user = await userManager.FindByIdAsync(CustAcc.Id);

                user.Id = CustAcc.Id;
                user.UserName = CustAcc.UserName;
                user.Email = CustAcc.Email;
                user.LName = CustAcc.LName;
                user.FName = CustAcc.FName;
                user.PhoneNumber = CustAcc.PhoneNumber;
                user.Address = CustAcc.Address;


                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Account {0} edited successfully.", CustAcc.UserName);
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


/*

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
        var result = await userManager.CreateAsync(user, RModel.Password);
        if (result.Succeeded)
        {
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
    return Page();




*/