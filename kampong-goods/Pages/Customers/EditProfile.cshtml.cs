using EllipticCurve.Utils;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using static System.Collections.Specialized.BitVector32;

namespace kampong_goods.Pages.Customers
{
    [Authorize(Roles = "Customer")]

    public class EditProfileModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly CustomerService _customerService;
/*
        [BindProperty]
        public EditProfile EModel { get; set; }*/
        public EditProfileModel(CustomerService customerService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            this.userManager = userManager;
            this.signInManager = signInManager;


        }

        [BindProperty]
        public AppUser CustProfile { get; set; } = new();

/*        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }*/
        public IActionResult OnGet(string id)
        {
/*                        System.Diagnostics.Debug.WriteLine(id);
*/          AppUser? customer = _customerService.GetCustomerById(id);


            if (customer != null)
            {
                CustProfile = customer;
                


                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Invalid access");
                return Redirect("/Customer");
            }
        }



        public async Task<IActionResult> OnPostAsync()
        {
            

            var custlist = await userManager.GetUsersInRoleAsync("Customer");


            if (Regex.IsMatch(CustProfile.Email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$") == true && Regex.IsMatch(CustProfile.PhoneNumber, @"^[89][0-9]{7}$")==true)
            {
                if (ModelState.IsValid)
                {

                    /*                System.Diagnostics.Debug.WriteLine("eh" + CustProfile.Id);
                    */
                    /* AppUser ser = await userManager.GetUserAsync(HttpContext.User);
                     System.Diagnostics.Debug.WriteLine("context" + ser.Id);*/

                    var user = await userManager.FindByIdAsync(CustProfile.Id);
                    var prevusername = user.UserName;
                    var prevemail = user.Email;


                    user.Id = CustProfile.Id;
                    user.UserName = CustProfile.UserName;
                    user.Email = CustProfile.Email;
                    user.LName = CustProfile.LName;
                    user.FName = CustProfile.FName;
                    user.PhoneNumber = CustProfile.PhoneNumber;
                    user.Address = CustProfile.Address;


                    if (user.UserName != prevusername)
                    {
                        foreach (var i in custlist)
                        {

                            /*                      System.Diagnostics.Debug.WriteLine("iname" + i.Id);

                                                  System.Diagnostics.Debug.WriteLine("user.Un" + user.UserName);
                                                  System.Diagnostics.Debug.WriteLine("iname" + i.UserName);*/
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
                        TempData["FlashMessage.Text"] = string.Format("Profile {0} edited successfully.", CustProfile.UserName);
                        await signInManager.SignInAsync(user, false);
                        return RedirectToPage("Profile");
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
