using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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




    }
}

