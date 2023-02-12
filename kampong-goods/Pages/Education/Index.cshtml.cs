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

        //FAQ retreieve
        public void OnGet()
        {
            FAQlist = _FAQService.GetAll();

        }

        //Publish
        public void OnGetPublishFAQ(string ID)
        {
            myFAQ = _FAQService.GetFAQById(ID);
            if (myFAQ.Publish == true)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} has Published", myFAQ.Question);
                FAQlist = _FAQService.GetAll();
            }
            else 
            {
                myFAQ.Publish = true;
                _FAQService.UpdateFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is Published", myFAQ.Question);
                FAQlist = _FAQService.GetAll();
            }
            
        }

        //Pull
        public void OnGetPullFAQ(string ID)
        {
            myFAQ = _FAQService.GetFAQById(ID);
            if (myFAQ.Publish == false)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} cannot be pulled if not published before", myFAQ.Question);
                FAQlist = _FAQService.GetAll();
            }
            else
            {
                myFAQ.Publish = false;
                _FAQService.UpdateFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is pulled from Webiste", myFAQ.Question);
                FAQlist = _FAQService.GetAll();
            }

        }


        //Delete
        public void OnGetDeleteFAQ(string ID)
        {
            _FAQService.DeleteFAQ(ID);
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format(
            "FAQ{0} is Delete", ID);
            FAQlist = _FAQService.GetAll();
        }


        // sort function
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


        //Post: Search Function;
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