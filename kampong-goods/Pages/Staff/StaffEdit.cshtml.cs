using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace kampong_goods.Pages.Staff
{
    public class StaffEditModel : PageModel
    {
        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly StaffService _staffService;

        private readonly CustomerService _customerService;



        public StaffEditModel(StaffService staffService, CustomerService customerService, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _staffService = staffService;
            _customerService = customerService;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

        }

        [BindProperty]
        public AppUser StaffProfile { get; set; } = new();

        [BindProperty]
        public StaffInfo Profile { get; set; } = new();



        public IActionResult OnGet(string id)
        {
            /*                        System.Diagnostics.Debug.WriteLine(id);
            */
            AppUser? staff = _staffService.GetCustomerById(id);


            if (staff != null)
            {
                /*                Profile.User = staff;
                */
                Profile = _staffService.GetICById(id)!;



                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Invalid access");
                return Redirect("/Staff/StaffLogin");
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var custlist = _customerService.GetAll();
            var stafflist = _staffService.GetAll();
            int inputcheck = 0;


            if (!Regex.IsMatch(Profile.User.Email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$"))
            {
                inputcheck += 1;
              ModelState.AddModelError("", "Invalid Email Format.");
            }
                
             if(!Regex.IsMatch(Profile.User.PhoneNumber, @"^[89][0-9]{7}$"))
            {
                inputcheck += 1;
                ModelState.AddModelError("", "Invalid Phone Number.");

            }


            if (!Regex.IsMatch(Profile.User.FName, @"^[A-Za-z]+$"))
            {
                inputcheck += 1;
                ModelState.AddModelError("", "First Name can only be in alphabets.");

            }

            if (!Regex.IsMatch(Profile.User.LName, @"^[A-Za-z]+$"))
            {
                inputcheck += 1;
                ModelState.AddModelError("", "Last Name can only be in alphabets.");

            }



            if (inputcheck==0)
            {
               /* if (ModelState.IsValid)
                {*/

                    var user = await userManager.FindByIdAsync(Profile.UserId);
                    var prevusername = user.UserName;
                    var prevemail = user.Email;
                    var previnfo = _staffService.GetICById(Profile.UserId);

                    user.Id = Profile.UserId;
                    user.UserName = Profile.User.UserName;
                    user.Email = Profile.User.Email;
                    user.LName = Profile.User.LName;
                    user.FName = Profile.User.FName;
                    user.PhoneNumber = Profile.User.PhoneNumber;
                    user.Address = Profile.User.Address;

                    var staffic = Profile.NRIC;

/*                    var ic = staff.NRIC;
*/
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

                            if (i.Email == user.Email && i.Id != user.Id)
                            {
                                TempData["FlashMessage.Type"] = "danger";
                                TempData["FlashMessage.Text"] = string.Format("Email {0} is in used already.", i.Email);
                                return Page();
                            }


                        }
                    }



                    if (staffic != previnfo.NRIC)
                    {
                        foreach (var i in stafflist)
                        {
                            /*                     System.Diagnostics.Debug.WriteLine("i" + i);
                                                 System.Diagnostics.Debug.WriteLine("i" + i.UserName);*/
                            if (i.NRIC == staffic && i.UserId != user.Id)
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
                    /*var updatestaff = new StaffInfo()
                    {

                        NRIC = Profile.NRIC,
                        User = user

                    };*/


                    previnfo.NRIC = Profile.NRIC;
                    previnfo.UserId = Profile.UserId;
                    previnfo.User.UserName = Profile.User.UserName;
                    previnfo.User.Email = Profile.User.Email;
                    previnfo.User.LName = Profile.User.LName;
                    previnfo.User.FName = Profile.User.FName;
                    previnfo.User.PhoneNumber = Profile.User.PhoneNumber;
                    previnfo.User.Address = Profile.User.Address;

                    _staffService.UpdateStaffInfo(previnfo);


                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Profile {0} edited successfully.", Profile.User.UserName);
                        return RedirectToPage("StaffProfile");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }





                /*}*/
            }

            return Page();
        }
    }


}
