using kampong_goods.Models;

namespace kampong_goods.Services
{
    public class VoucherService
    {
        private readonly VoucherDbContext _context;

        public VoucherService(VoucherDbContext context)
        {
            _context = context;
        }

        public List<Voucher> GetAll()
        {
            return _context.Vouchers.OrderBy(m => m.VoucherId).ToList();
        }

        public Voucher? GetVoucherById(string id)
        {
            Voucher? voucher = _context.Vouchers.FirstOrDefault(x => x.VoucherId.Equals(id));
            return voucher;
        }

        public void AddVoucher(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            _context.SaveChanges();
        }

        public void UpdateVoucher(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            _context.SaveChanges();
        }

        public void DeleteVoucher(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            _context.SaveChanges();
        }
    }
}
