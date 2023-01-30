using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Claims;

namespace kampong_goods.Pages.Requests
{
    public class AddRequestModel : PageModel
    {
        private readonly CategoryService _categoryService;
        private readonly RequestService _requestService;
        private readonly CustomerService _customerService;



        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;
        public AddRequestModel(CategoryService categoryService, RequestService requestService, CustomerService customerService)
           
        {
            _customerService = customerService;
            _categoryService = categoryService;
            _requestService = requestService;

        }

        [BindProperty]
        public AddRequest ReqModel { get; set; }


/*        [BindProperty]
        public Request MyRequest { get; set; } = new();*/

        public static List<Category> CategoryList { get; set; } = new();

        public void OnGet()
        {
            CategoryList = _categoryService.GetAll();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                Debug.WriteLine(userid);
                var user = _customerService.GetCustomerById(userid);

                var req = new Request()
                {

                    ReqTitle = ReqModel.ReqTitle,
                    Description=ReqModel.Description,
                    CategoryId=ReqModel.CategoryId,
                    Budget= ReqModel.Budget,
                    User = user,

                };

                _requestService.AddRequest(req);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Request {0} successfully published.", ReqModel.ReqTitle);
                return Redirect("/Requests/AllRequests");

                /*                await signInManager.SignInAsync(user, false);
                                return RedirectToPage("Dashboard");*/

                /*                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                                Debug.WriteLine(userid);
                                MyRequest.UserId = userid;*/

                //check id
/*                Request? request = _requestService.GetRequestById(MyRequest.Id);
*//*                if (product != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Product ID {0} already exists", MyProduct.ProductId);
                    return Page();
                }


*//*                _requestService.AddRe(MyProduct);
*//*                TempData["FlashMessage.Type"] = "Success";
                TempData["FlashMessage.Text"] = string.Format("Request {0} published, MyRequest.reqTitle);
                return Redirect("/Requests/AllRequests");*/
            }
            return Page();
        }


    }
}








    