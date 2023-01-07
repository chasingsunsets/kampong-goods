using kampong_goods.Models;

namespace kampong_goods.Services
{
    public class VoucherService
    {
        private static List<Voucher> AllVouchers = new()
        {
            new Voucher
            {
                VoucherId = "1",
                VoucherName = "May Tan",
                VoucherDiscAmt = 5,
                VoucherMinSpend = 11,
                VoucherCode = "REDEEMNOW",
                VoucherExpDate = DateTime.Parse("11/11/1980"),
            },
            new Voucher
            {
                VoucherId = "2",
                VoucherName = "John Lim",
                VoucherDiscAmt = 10,
                VoucherMinSpend = 10,
                VoucherCode = "123",
                VoucherExpDate = DateTime.Parse("01/11/1981")
            },
            new Voucher
            {
                VoucherId = "3",
                VoucherName = "Joann Tan",
                VoucherDiscAmt = 25,
                VoucherMinSpend = 5,
                VoucherCode = "ABC",
                VoucherExpDate = DateTime.Parse("11/11/1990"),
            },
            new Voucher
            {
                VoucherId = "4",
                VoucherName = "Peter Ang",
                VoucherDiscAmt = 45,
                VoucherMinSpend = 15,
                VoucherCode = "REDEEMLATER",
                VoucherExpDate = DateTime.Parse("01/11/1991"),
            },
        };

        public List<Voucher> GetAll()
        {
            return AllVouchers.OrderBy(x => x.VoucherId).ToList();
        }

        public Voucher? GetVoucherById(string id)
        {
            Voucher? voucher = AllVouchers.FirstOrDefault(x => x.VoucherId.Equals(id));
            return voucher;
        }

        public void AddVoucher(Voucher voucher)
        {
            AllVouchers.Add(voucher);
        }
    }
}
