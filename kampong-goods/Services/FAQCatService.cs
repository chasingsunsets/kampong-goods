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
            return _context.FAQCategorys.OrderBy(m => m.FAQCatId).ToList();
            //return AllFAQs.OrderBy(a => a.ID).ToList();
        }

        public FAQCategory? GetFAQCatById(string ID)
        {
            FAQCategory? faqCat = _context.FAQCategorys.FirstOrDefault(x => x.FAQCatId.Equals(ID));
            //FAQ? faq = AllFAQs.FirstOrDefault(x => x.ID.Equals(ID));
            return faqCat;
        }
        public FAQCategory? GetIdByFAQCat(FAQCategory Category)
        {
            FAQCategory? faqCat = _context.FAQCategorys.FirstOrDefault(x => x.FAQCategoryName.Equals(Category));
            //FAQ? faq = AllFAQs.FirstOrDefault(x => x.ID.Equals(ID));
            return faqCat;
        }
        public void AddFAQCAt(FAQCategory faqCat)
        {
            _context.FAQCategorys.Add(faqCat);
            _context.SaveChanges();
            //AllFAQs.Add(faq);
        }
        public void UpdateFAQCat(FAQCategory faqCat)
        {
            _context.FAQCategorys.Update(faqCat);
            _context.SaveChanges();
        }

        public void DeleteFAQCat(string ID)
        {
            var fAQCategory = _context.FAQCategorys.FirstOrDefault(x => x.FAQCatId == ID);
            /*var fAQCategory = GetFAQCatById(ID);*/
            _context.FAQCategorys.Remove(fAQCategory);
            _context.SaveChanges();
        }
    }
}
