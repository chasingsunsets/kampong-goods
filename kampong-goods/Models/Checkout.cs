namespace kampong_goods.Models
{
	public class Checkout
	{
		public string CheckoutId { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string CCName { get; set; }
		public string CCNo { get; set; }
		public DateTime? Expiration { get; set; }
		public string CVV { get; set; }
		public string ProductId { get; set; }
	}
}
