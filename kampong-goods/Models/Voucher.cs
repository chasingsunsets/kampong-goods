using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class Voucher
    {
        [MaxLength(1000)]
        public string VoucherId { get; set; } = string.Empty;

        [Required, MinLength(3, ErrorMessage = "Enter at least 3 characters."), MaxLength(30)]
        public string VoucherName { get; set; } = string.Empty;

        [Required, Range(1, 1000)]
        public decimal VoucherDiscAmt { get; set; }

        [Required, Range(0, 100)]
        public decimal VoucherMinSpend { get; set; }

        [Required, MaxLength(8)]
        public string VoucherCode { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Expiry")]
        public DateTime VoucherExpDate { get; set; } = new DateTime(DateTime.Now.Year - 0, 1, 1);

    }
}
