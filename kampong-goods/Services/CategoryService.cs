using kampong_goods.Models;

namespace kampong_goods.Services
{
	public class CategoryService
	{
        private readonly ProductDbContext _context;

        public CategoryService(ProductDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.OrderBy(d => d.CategoryName).ToList();
        }

        public Category? GetCategoryById(string id)
        {
            Category? category = _context.Categories.FirstOrDefault(
                x => x.CategoryId == id);
            return category;
        }
    }
}
