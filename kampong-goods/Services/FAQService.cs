using Microsoft.EntityFrameworkCore;
using kampong_goods.Models;

namespace kampong_goods.Services
{
    public class FAQService
    {
        //private static List<FAQ> AllFAQs = new()
        //{
        //    new FAQ{ID="0", Question="Q1", Answer="A1", Creator="Captain45", 
        //        Editor="Captain45", URL="null", Date_Created="10/1/2023"},
        //    new FAQ{ID="1", Question="Q2", Answer="A2", Creator="Captain45",
        //        Editor="Captain45", URL="null", Date_Created="10/1/2023"},
        //    new FAQ{ID="2", Question="Q3", Answer="A3", Creator="Captain45",
        //        Editor="Captain45", URL="null", Date_Created="10/1/2023"},
        //};

        private readonly FAQDbContext _context;
        public FAQService(FAQDbContext context)
        {
            _context = context;
        }

        public List<FAQ> GetAll()
        {
            return _context.FAQs.OrderBy(m => m.FAQId).ToList();
            //return AllFAQs.OrderBy(a => a.ID).ToList();
        }

        public FAQ? GetFAQById(string ID)
        {
            FAQ? faq = _context.FAQs.FirstOrDefault(x => x.FAQId.Equals(ID));
            //FAQ? faq = AllFAQs.FirstOrDefault(x => x.ID.Equals(ID));
            return faq;
        }

        public void AddFAQ(FAQ faq)
        {
            _context.FAQs.Add(faq);
            _context.SaveChanges();
            //AllFAQs.Add(faq);
        }
        public void UpdateFAQ(FAQ faq)
        {
            _context.FAQs.Update(faq);
            _context.SaveChanges();
        }

        public void DeleteFAQ(string ID)
        {
            var FAQ = GetFAQById(ID);
            _context.FAQs.Remove(FAQ);
            _context.SaveChanges();
        }
    }
}


