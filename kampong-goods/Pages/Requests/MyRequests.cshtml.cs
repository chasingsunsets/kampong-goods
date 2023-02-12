using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Requests
{
    public class MyRequestsModel : PageModel
    {
        private readonly RequestService _requestService;
        private readonly CustomerService _customerService;
        private readonly CategoryService _categoryService;


        public MyRequestsModel(CategoryService categoryService, RequestService requestService, CustomerService customerService)
        {
            _requestService = requestService;
            _customerService = customerService;
            _categoryService = categoryService;

        }

        public List<Request> RequestList { get; set; } = new();
        public List<AppUser> CustList { get; set; } = new();
        public static List<Category> CategoryList { get; set; } = new();


        public void OnGet()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            System.Diagnostics.Debug.WriteLine("me" + userid);
            if (userid != null)
            { RequestList = _requestService.GetMyNotA(userid); }


            CustList = _customerService.GetAll();
            CategoryList = _categoryService.GetAll();

        }

        public async Task<IActionResult> OnGetDeleteR(int id)
        {
            if (id == null)
            {
                return Page();
            }


            var request = _requestService.GetRequestById(id);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            Debug.WriteLine(userid);
            var user = _customerService.GetCustomerById(userid);

            /*            var test = await userManager.FindByIdAsync("84ae787f-18c5-41f9-9bfd-1e85b5d13778");
                        System.Diagnostics.Debug.WriteLine("hard" + test);*/
            /*            var user = _customerService.GetCustomerById(id);
                        System.Diagnostics.Debug.WriteLine("await" + user);*/



            /*            var user = await userManager.FindByIdAsync(username.Id);
            */

            if (request != null)
            {
                if (request.UserId != userid)
                {

                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Invalid access, you can only delete the request your account have.");
                    return Page();
                }

                else
                {
                    _requestService.DeleteRequest(request);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("Request deleted.");
                        return RedirectToPage("/Requests/MyRequests");
                    }
            }
            return RedirectToPage();


        }
    }
}
