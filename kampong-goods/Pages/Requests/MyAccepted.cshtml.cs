using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Requests
{
    public class MyAcceptedModel : PageModel
    {
        private readonly RequestService _requestService;
        private readonly CustomerService _customerService;
        private readonly CategoryService _categoryService;



        public MyAcceptedModel(CategoryService categoryService, RequestService requestService, CustomerService customerService)
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
            { RequestList = _requestService.GetMyA(userid); }

          
            CustList = _customerService.GetAll();
            CategoryList = _categoryService.GetAll();

        }


        public async Task<IActionResult> OnGetDeleteA(int id)
        {
            if (id == null)
            {
                return Page();
            }


            var request = _requestService.GetRequestById(id);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            Debug.WriteLine(userid);
            var user = _customerService.GetCustomerById(userid);


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
                    return Page();
                }
            }
            return RedirectToPage();


        }

    }
}

