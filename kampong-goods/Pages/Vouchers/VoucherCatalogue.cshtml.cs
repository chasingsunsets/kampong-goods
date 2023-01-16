using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;

namespace kampong_goods.Pages.Vouchers
{
    public class VoucherCatalogueModel : PageModel
    {
        private readonly VoucherService _voucherService;

        public VoucherCatalogueModel(VoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public List<Voucher> VoucherList { get; set; } = new();
        public Voucher MyVoucher { get; set; } = new();


        public void OnGet()
        {
            VoucherList = _voucherService.GetAll();
        }
    }
}
