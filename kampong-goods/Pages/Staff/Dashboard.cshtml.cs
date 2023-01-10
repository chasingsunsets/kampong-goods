using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace kampong_goods.Pages.Staff
{
    [Authorize(Roles ="Staff")]
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

