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

        public List<Product> GetAll()
        {
            return _context.Products.OrderBy(m => m.ProductName).ToList();
        }

        public List<Checkout> GetAllCheckouts()
        {
            return _context.Checkout.OrderBy(m => m.CheckoutId).ToList();
        }

        public Checkout? GetOrderById(string id)
        {
            Checkout? checkout = _context.Checkout.FirstOrDefault(x => x.CheckoutId.Equals(id));
            return checkout;
        }

        public void AddOrder(Checkout checkout)
        {
            _context.Checkout.Add(checkout);
            _context.SaveChanges();
        }

        public void UpdateCheckout(Checkout checkout)
        {
            _context.Checkout.Update(checkout);
            _context.SaveChanges();
        }

        public void DeleteCheckout(Checkout checkout)
        {
            _context.Checkout.Remove(checkout);
            _context.SaveChanges();
        }
    }
}
