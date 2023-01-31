using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Vouchers
{
    //[Authorize(Roles ="Staff")]
    public class IndexModel : PageModel
    {
        private readonly VoucherService _voucherService;

        public IndexModel(VoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public List<Voucher> VoucherList { get; set; } = new();
        public Voucher MyVoucher { get; set; } = new();


        public void OnGet()
        {
            VoucherList = _voucherService.GetAll();
        }

        //public IActionResult OnPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _voucherService.DeleteVoucher(MyVoucher);
        //    }
        //    return Page();
        //}

        public async Task<IActionResult> OnGetDelete(string id)
        {
            if (id == null)
            {
                return Page();
            }

            var voucher = _voucherService.GetVoucherById(id);
            System.Diagnostics.Debug.WriteLine("await" + voucher);

            if (voucher != null)
            {
                if (voucher.VoucherId.ToString() != id)
                {

                    //TempData["FlashMessage.Type"] = "danger";
                    //TempData["FlashMessage.Text"] = string.Format("Invalid access, you can only delete the product you logged in with.");
                    return Page();
                }

                else
                {
                    _voucherService.DeleteVoucher(voucher);
                    //TempData["FlashMessage.Type"] = "success";
                    //TempData["FlashMessage.Text"] = string.Format("Product {0} is deleted", product.ProductName);
                }

            }

            return RedirectToPage();
        }
    }
}
