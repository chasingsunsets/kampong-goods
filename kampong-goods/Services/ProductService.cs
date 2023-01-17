using kampong_goods.Models;

namespace kampong_goods.Services
{
	public class ProductService
	{
        private readonly ProductDbContext _context;

        public ProductService(ProductDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.OrderBy(m => m.ProductName).ToList();
        }

        public Product? GetProductById(string id)
        {
            Product? product = _context.Products.FirstOrDefault(x => x.ProductId.Equals(id));
            return product;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
