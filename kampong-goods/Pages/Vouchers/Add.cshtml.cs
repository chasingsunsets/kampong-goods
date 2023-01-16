using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;

namespace kampong_goods.Pages.Vouchers
{
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
            // Test Data
            //MyEmployee.EmployeeId = "MAYT";
            //MyEmployee.NRIC = "S1111111D";
            //MyEmployee.Name = "May Tan";
            //MyEmployee.Gender = "F";
            //MyEmployee.DepartmentId = "IT";
            //MyEmployee.Salary = 5000;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Guid myuuid = Guid.NewGuid();
                //MyVoucher.VoucherId = myuuid.ToString();
                Voucher? voucher = _voucherService.GetVoucherById(MyVoucher.VoucherId);
                if (voucher != null)
                {
                    ModelState.AddModelError("MyVoucher.VoucherId", "Voucher ID already exists.");
                    return Page();
                }

                _voucherService.AddVoucher(MyVoucher);
                return Redirect("/Vouchers");
            }
            return Page();
        }
    }
}
