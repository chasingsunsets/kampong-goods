using kampong_goods.Models;

namespace kampong_goods.Services
{
    public class FAQCatService
    {
        private readonly FAQDbContext _context;
        public FAQCatService(FAQDbContext context)
        {
            _context = context;
        }

        public List<FAQCategory> GetAll()
        {
            return _context.FAQCategories.OrderBy(m => m.Id).ToList();
            //return AllFAQs.OrderBy(a => a.ID).ToList();
        }

        public FAQCategory? GetFAQCatById(int ID)
        {
            FAQCategory? faqCat = _context.FAQCategories.FirstOrDefault(x => x.Id.Equals(ID));
            //FAQ? faq = AllFAQs.FirstOrDefault(x => x.ID.Equals(ID));
            return faqCat;
        }
        public void AddFAQCAt(FAQCategory faqCat)
        {
            _context.FAQCategories.Add(faqCat);
            _context.SaveChanges();
            //AllFAQs.Add(faq);
        }
        public void UpdateFAQCat(FAQCategory faqCat)
        {
            _context.FAQCategories.Update(faqCat);
            _context.SaveChanges();
        }

        public void DeleteFAQCat(int ID)
        {
            var fAQCategory = GetFAQCatById(ID);
            _context.FAQCategories.Remove(fAQCategory);
            _context.SaveChanges();
        }
    }
}
