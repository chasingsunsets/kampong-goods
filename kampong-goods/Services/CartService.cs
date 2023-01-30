using kampong_goods.Models;

namespace kampong_goods.Services
{
	public class CartService
	{
        private readonly ProductDbContext _context;

        public CartService(ProductDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.OrderBy(m => m.ProductName).ToList();
        }

        public List<Cart> GetAllCarts()
        {
            return _context.Carts.OrderBy(m => m.CartId).ToList();
        }

        public Cart? GetCartById(string id)
        {
            Cart? cart = _context.Carts.FirstOrDefault(x => x.CartId.Equals(id));
            return cart;
        }

        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }

        public void DeleteCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }
}
