/*using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace kampong_goods.Pages.Staff
{
    public class EditStaffListModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly CustomerService _customerService;


        public EditStaffListModel(CustomerService customerService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            this.userManager = userManager;
            this.signInManager = signInManager;


        }

        [BindProperty]
        public AppUser StaffAcc { get; set; } = new();



        public IActionResult OnGet(string id)
        {

            AppUser? staff = _customerService.GetCustomerById(id);
            System.Diagnostics.Debug.WriteLine(id);


            if (staff != null)
            {
                StaffAcc = staff;
                System.Diagnostics.Debug.WriteLine(staff.Id);


                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Staff {0} not found", id);
                return RedirectToPage("StaffList");
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var stafflist = await userManager.GetUsersInRoleAsync("Staff");

            if (Regex.IsMatch(StaffAcc.NRIC, @"^[STFG]\d{7}[A-Z]$") == true && Regex.IsMatch(StaffAcc.PhoneNumber, @"^[89][0-9]{7}$") == true)
            {
                if (ModelState.IsValid)
                {



                    *//*                var user = new AppUser()
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
                                    System.Diagnostics.Debug.WriteLine("fix?" + CustAcc.UserName);*//*

                    var user = await userManager.FindByIdAsync(StaffAcc.Id);
                    var prevusername = user.UserName;
                    var prevnric = user.NRIC;


                    user.Id = StaffAcc.Id;
                    user.UserName = StaffAcc.UserName;
                    user.NRIC = StaffAcc.NRIC;
                    user.LName = StaffAcc.LName;
                    user.FName = StaffAcc.FName;
                    user.PhoneNumber = StaffAcc.PhoneNumber;

                    if (user.UserName != prevusername)
                    {
                        foreach (var i in stafflist)
                        {

                            if (i.UserName == user.UserName && i.Id != user.Id)
                            {
                                *//*                            System.Diagnostics.Debug.WriteLine("user.Un" + user.UserName);
                                                            System.Diagnostics.Debug.WriteLine("iname" + i.UserName);*//*
                                TempData["FlashMessage.Type"] = "danger";
                                TempData["FlashMessage.Text"] = string.Format("Username {0} is in used already.", i.UserName);
                                return Page();
                            }

                        }
                    }

                    if (user.NRIC != prevnric)
                    {
                        foreach (var i in stafflist)
                        {
                            *//*                     System.Diagnostics.Debug.WriteLine("i" + i);
                                                 System.Diagnostics.Debug.WriteLine("i" + i.UserName);*//*
                            if (i.NRIC == user.NRIC && i.Id != user.Id)
                            {
                                TempData["FlashMessage.Type"] = "danger";
                                TempData["FlashMessage.Text"] = string.Format("NRIC {0} is in used already.", i.NRIC);
                                return Page();
                            }


                        }
                    }


                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Account {0} edited successfully.", StaffAcc.UserName);
                        return RedirectToPage("StaffList");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }





            }

            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("Invalid NRIC or PhoneNumber");
            return Page();

        }









    }
}
*/