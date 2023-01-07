using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;

namespace kampong_goods.Pages.Vouchers
{
    public class DetailsModel : PageModel
    {
        private readonly VoucherService _voucherService;
        public DetailsModel(VoucherService voucherService)
        { _voucherService = voucherService; }

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
    }
}
