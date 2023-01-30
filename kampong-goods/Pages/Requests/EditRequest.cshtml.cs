using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace kampong_goods.Pages.Requests
{
    public class EditRequestModel : PageModel
    {
        private readonly RequestService _requestService;
        private readonly CustomerService _customerService;
        private readonly CategoryService _categoryService;


        public EditRequestModel(CategoryService categoryService, RequestService requestService, CustomerService customerService)
        {
            _requestService = requestService;
            _customerService = customerService;
            _categoryService = categoryService;

        }

        public List<Request> RequestList { get; set; } = new();
        public List<AppUser> CustList { get; set; } = new();
        public static List<Category> CategoryList { get; set; } = new();


        [BindProperty]
        public Request MyRequest { get; set; } = new();

        [BindProperty]
        public AddRequest ReqModel { get; set; }


        /*        [BindProperty]
                public Request MyRequest { get; set; } = new();*/


/*        public void OnGet()
        {
            CategoryList = _categoryService.GetAll();
            if (CategoryList.Count == 0)
            { System.Diagnostics.Debug.WriteLine("EMPTYYY"); }

        }*/

        public IActionResult OnGet(int id)
        {
/*            CategoryList = _categoryService.GetAll();
            if (CategoryList.Count == 0)
            { System.Diagnostics.Debug.WriteLine("EMPTYYY"); }
            else { System.Diagnostics.Debug.WriteLine("HVVV"); }*/

            Request? request = _requestService.GetRequestById(id);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            if (request != null)
            {
                if (request.UserId == userid)
                {
                    MyRequest = request;
                    CategoryList = _categoryService.GetAll();
                    return Page();

                }
                else
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Invalid access");
                    return Redirect("/Requests/MyRequests");
                }


            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Request doesn't exists");
                return Redirect("/Requests/MyRequests");
            }
        }
    }
}
