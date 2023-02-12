using kampong_goods.Migrations;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace kampong_goods.Pages.Requests
{
    public class AllRequestsModel : PageModel
    {
        private readonly RequestService _requestService;
        private readonly CustomerService _customerService;
        private readonly CategoryService _categoryService;



        public AllRequestsModel(CategoryService categoryService, RequestService requestService, CustomerService customerService)
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
            System.Diagnostics.Debug.WriteLine("me"+userid);
            if (userid != null) 
            { exclRequestList = _requestService.GetNotMy(userid);
                System.Diagnostics.Debug.WriteLine("xx");
            }

            else { exclRequestList = _requestService.GetAll();
                System.Diagnostics.Debug.WriteLine("here");

            }
            /*            RequestList = _requestService.GetAll();
                        if (userid != null)
                        {
                            foreach (var r in RequestList)
                            {
                                System.Diagnostics.Debug.WriteLine(r);
                                System.Diagnostics.Debug.WriteLine("ruser" + r.UserId);

                                if (r.UserId != userid && r.Status=="Available")

                                {
                                    exclRequestList.Add(r);
                                    System.Diagnostics.Debug.WriteLine("added" + r);

                                };
                            }
                        }

                        else
                        {
                            System.Diagnostics.Debug.WriteLine("no user logged");


                            foreach (var r in RequestList)
                            {

                                if (r.Status == "Available")

                                {
                                    exclRequestList.Add(r);

                                };
                            }
                        }*/
            /* if (exclRequestList.Count==0)
             {
                 System.Diagnostics.Debug.WriteLine("await");

                 exclRequestList = _requestService.GetAll();
             }*/
            CustList = _customerService.GetAll();
            CategoryList = _categoryService.GetAll();

        }


        public async Task<IActionResult> OnGetAccept(int id)
        {
            if (id.ToString() == null)
            {
                return Page();
            }
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userid == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Please log in to accept requests.");
                return RedirectToPage();

            }
            Request? request = _requestService.GetRequestById(id);
            var user = _customerService.GetCustomerById(request.UserId);


            if (user != null)
            {
                if (userid == request.UserId)
                {

                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Error, cannot accept your own request.");
                    return Page();
                }
                else
                {

                    request.Status = userid;
                    _requestService.UpdateRequest(request);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Request accepted.");
                    return Redirect("/Requests/IAccept");


                }
            }
            

            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format("No user found.");

            return RedirectToPage();
        }



    }
}
