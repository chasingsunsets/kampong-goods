using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Customers
{
    public class CustListModel : PageModel
    {

        private readonly CustomerService _customerService;
        public CustListModel(CustomerService customerService)
        {
            _customerService = customerService;
        }
        public List<AppUser> CustList { get; set; } = new();


        public void OnGet()
        {
            CustList = _customerService.GetAll();
        }
    }
}
