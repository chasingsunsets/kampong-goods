using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace kampong_goods.Pages.Staff
{
    /*[Authorize(Policy = "MustBeStaff")]*/
    [Authorize(AuthenticationSchemes = "AdminAuth")]

    /*    [Authorize(Roles = "Staff")]
    */
    /*    [Authorize(Roles = "Staff", AuthenticationSchemes = "AdminAuth")]
    */

    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

