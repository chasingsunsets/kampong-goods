using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace kampong_goods.Pages.Requests
{
    public class IAcceptModel : PageModel
    {
        private readonly RequestService _requestService;
        private readonly CustomerService _customerService;
        private readonly CategoryService _categoryService;

        public IAcceptModel(CategoryService categoryService, RequestService requestService, CustomerService customerService)
        {
            _requestService = requestService;
            _customerService = customerService;
            _categoryService = categoryService;

        }

        public List<Request> RequestList { get; set; } = new();
        public List<Request> exclRequestList { get; set; } = new();

        public List<AppUser> CustList { get; set; } = new();
        public static List<Category> CategoryList { get; set; } = new();


        public void OnGet()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            RequestList = _requestService.GetIAccepted(userid);
            CustList = _customerService.GetAll();
            CategoryList = _categoryService.GetAll();

        }


        public async Task<IActionResult> OnGetUnAccept(int id)
        {
            if (id.ToString() == null)
            {
                return Page();
            }
            Request? request = _requestService.GetRequestById(id);
            var user = _customerService.GetCustomerById(request.UserId);

            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user != null)
            {
               
                    request.Status = "Available";

                    _requestService.UpdateRequest(request);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Request unaccepted.");
                    return Redirect("/Requests/IAccept");


            }

            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("No user found.");
            return RedirectToPage();
        }



    }
}