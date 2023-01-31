using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Xml.Linq;


namespace kampong_goods.Pages.Education
{
    public class IndexModel : PageModel
    {
        private readonly FAQService _FAQService;
        public FAQ myFAQ { get; set; } = new();
        public string searchItem { get; set; }
        

        public List<FAQ> FAQlist { get; set; } = new();

        public List<FAQ> FAQlist_sorted { get; set;} = new();
        public IndexModel(FAQService faqService)
        {
            _FAQService = faqService;
        }

        public void OnGet()
        {
            FAQlist = _FAQService.GetAll();

        }

        public void OnGetDeleteFAQ(string ID)
        {
            _FAQService.DeleteFAQ(ID);
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format(
            "FAQ{0} is Delete", ID);
            FAQlist = _FAQService.GetAll();
        }

        public void OnGetSortFAQAesc(string ID)
        {
            FAQlist = _FAQService.GetAll();
            FAQlist_sorted = FAQlist.OrderBy(x => x.Date_Created).ToList();
            FAQlist = FAQlist_sorted;
        }

        public void OnGetSortFAQDesc(string ID)
        {
            FAQlist = _FAQService.GetAll();
            FAQlist_sorted = FAQlist.OrderByDescending(x => x.Date_Created).ToList();
            FAQlist = FAQlist_sorted;
        }



        public IActionResult OnPost(string searchItem)
        {
            FAQlist = _FAQService.GetAll();
            if (string.IsNullOrEmpty(searchItem)) 
            {
                return Redirect("/Education");
            }else
            {
                FAQlist_sorted = FAQlist.Where(x => x.Question.Contains(searchItem) || x.Answer.Contains(searchItem)).ToList();
                FAQlist = FAQlist_sorted;
                return Page();
            }
          
            
        }




       
    }

}