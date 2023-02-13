using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Vouchers
{
    [Authorize(Roles = "Staff")]
    public class DetailsModel : PageModel
    {
        private readonly VoucherService _voucherService;
        public DetailsModel(VoucherService voucherService)
        { _voucherService = voucherService; }

        [BindProperty]
        public Voucher MyVoucher { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            Voucher? voucher = _voucherService.GetVoucherById(id);
            if (voucher != null)
            {
                MyVoucher = voucher;
                return Page();
            }
            else
            {
                return Redirect("/Vouchers");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Voucher? voucher = MyVoucher;
                _voucherService.UpdateVoucher(MyVoucher);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} is updated", MyVoucher.VoucherName);
                return Redirect("/Vouchers");
            }
            return Page();
        }
    }
}
