using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kampong_goods.Models
{
    public class Voucher
    {
        [MaxLength(1000)]
        [Display(Name = "Voucher ID")]
        public string VoucherId { get; set; } = string.Empty;

        [Required, MinLength(3, ErrorMessage = "Enter at least 3 characters."), MaxLength(30)]
        [Display(Name = "Voucher Name")]
        public string VoucherName { get; set; } = string.Empty;

        [Required, Range(1, 1000)]
        [Display(Name = "Discount Amount")]
        public decimal VoucherDiscAmt { get; set; }

        [Required, Range(0, 100)]
        [Display(Name = "Minimum Spend")]
        public decimal VoucherMinSpend { get; set; }

        [Required, MaxLength(8)]
        [Display(Name = "Voucher Code")]
        public string VoucherCode { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        [Column(TypeName = "date")]
        public DateTime VoucherExpDate { get; set; } = new DateTime(DateTime.Now.Year - 0, 1, 1);

    }
}
