using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Vouchers
{
    [Authorize(Roles = "Staff")]
    public class AddModel : PageModel
    {
        private readonly VoucherService _voucherService;

        public AddModel(VoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [BindProperty]
        public Voucher MyVoucher { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Voucher? voucher = _voucherService.GetVoucherById(MyVoucher.VoucherId);
                if (voucher != null)
                {
                    ModelState.AddModelError("MyVoucher.VoucherId", "Voucher ID already exists.");
                    return Page();
                }

                _voucherService.AddVoucher(MyVoucher);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} is added", MyVoucher.VoucherName);
                return Redirect("/Vouchers");
            }
            return Page();
        }
    }
}
