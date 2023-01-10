using kampong_goods.Models;
using kampong_goods.Pages.Customers;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace kampong_goods.Pages.Customers

{
    [Authorize(Roles = "Staff")]

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



        public IActionResult OnGet(string id)
        {

            AppUser? customer = _customerService.GetCustomerById(id);
            System.Diagnostics.Debug.WriteLine(id);


            if (customer != null)
            {
                CustAcc = customer;
                System.Diagnostics.Debug.WriteLine(customer.Id);


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
            var custlist = await userManager.GetUsersInRoleAsync("Customer");

            if (Regex.IsMatch(CustAcc.Email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$") == true && Regex.IsMatch(CustAcc.PhoneNumber, @"^[89][0-9]{7}$") == true)
            {
                if (ModelState.IsValid)
                {



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

                    /*                System.Diagnostics.Debug.WriteLine("fix?" + CustAcc.Id);
                                    System.Diagnostics.Debug.WriteLine("fix?" + CustAcc.UserName);*/

                    var user = await userManager.FindByIdAsync(CustAcc.Id);
                    var prevusername = user.UserName;
                    var prevemail = user.Email;


                    user.Id = CustAcc.Id;
                    user.UserName = CustAcc.UserName;
                    user.Email = CustAcc.Email;
                    user.LName = CustAcc.LName;
                    user.FName = CustAcc.FName;
                    user.PhoneNumber = CustAcc.PhoneNumber;
                    user.Address = CustAcc.Address;

                    if (user.UserName != prevusername)
                    {
                        foreach (var i in custlist)
                        {

                            if (i.UserName == user.UserName && i.Id != user.Id)
                            {
                                /*                            System.Diagnostics.Debug.WriteLine("user.Un" + user.UserName);
                                                            System.Diagnostics.Debug.WriteLine("iname" + i.UserName);*/
                                TempData["FlashMessage.Type"] = "danger";
                                TempData["FlashMessage.Text"] = string.Format("Username {0} is in used already.", i.UserName);
                                return Page();
                            }

                        }
                    }

                    if (user.Email != prevemail)
                    {
                        foreach (var i in custlist)
                        {
                            /*                     System.Diagnostics.Debug.WriteLine("i" + i);
                                                 System.Diagnostics.Debug.WriteLine("i" + i.UserName);*/
                            if (i.Email == user.Email && i.Id != user.Id)
                            {
                                TempData["FlashMessage.Type"] = "danger";
                                TempData["FlashMessage.Text"] = string.Format("Email {0} is in used already.", i.Email);
                                return Page();
                            }


                        }
                    }


                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account {0} edited successfully.", CustAcc.UserName);
                        return RedirectToPage("CustList");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
               



               
            }

            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("Invalid Email or PhoneNumber");
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