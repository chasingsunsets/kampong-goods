using kampong_goods.Models;

namespace kampong_goods.Services
{
	public class CheckoutService
	{
		private readonly ProductDbContext _context;
		public CheckoutService(ProductDbContext context)
		{
			_context = context;
		}
        public List<Checkout> GetAll()
        {
            return _context.Checkouts.OrderBy(m => m.CheckoutId).ToList();
        }
        public Checkout? GetCheckoutById(string id)
        {
            Checkout? checkout = _context.Checkouts.FirstOrDefault(
            x => x.CheckoutId.Equals(id));
            return checkout;
        }
        public void AddCheckout(Checkout checkout)
        {
            _context.Checkouts.Add(checkout);
            _context.SaveChanges();
        }
        public void UpdateCheckout(Checkout checkout)
        {
            _context.Checkouts.Update(checkout);
            _context.SaveChanges();
        }
    }
}
