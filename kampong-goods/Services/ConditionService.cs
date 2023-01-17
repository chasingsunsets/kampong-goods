using kampong_goods.Models;

namespace kampong_goods.Services
{
	public class ConditionService
	{
        private readonly ProductDbContext _context;

        public ConditionService(ProductDbContext context)
        {
            _context = context;
        }

        public List<Condition> GetAll()
        {
            return _context.Conditions.OrderBy(d => d.ConditionName).ToList();
        }

        public Condition? GetConditionById(string id)
        {
            Condition? condition = _context.Conditions.FirstOrDefault(
                x => x.ConditionId == id);
            return condition;
        }
    }
}
