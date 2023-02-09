using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kampong_goods.Models
{
	public class Checkout
	{
		public string CheckoutId { get; set; }

		[Required, MaxLength(50)]
		public string FName { get; set; }

        [Required, MaxLength(50)]
        public string LName { get; set; }

        [Required, RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required, RegularExpression(@"^[89][0-9]{7}$", ErrorMessage = "Invalid Phone Number"), MaxLength(8)]
        public string Phone { get; set; }

		[Required, MaxLength(100)]
		public string Address { get; set; }

        [Required, MaxLength(50)]
        public string CCName { get; set; }

        [Required, MaxLength(19)]
        public string CCNo { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }

        [Required, MaxLength(3)]
		public string CVV { get; set; }

		public string ProductId { get; set; }
	}
}
