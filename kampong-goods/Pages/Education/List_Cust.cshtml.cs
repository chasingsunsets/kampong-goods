using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Xml.Linq;

namespace kampong_goods.Pages.Education
{
    public class List_CustModel : PageModel
    {

        private readonly FAQService _FAQService;
        private readonly FAQCatService _FAQCatService;

        public FAQ myFAQ { get; set; } = new();

        public List<FAQ> FAQlist { get; set; } = new();
        public List<FAQCategory> FAQCatlist { get; set;} = new();

        //top 5 FAQ var
        public List<FAQ> FAQlist_sorted { get; set; } = new();
        public List<FAQ> FAQlist_top5 { get; set; } = new();
        public List_CustModel(FAQService faqService, FAQCatService faqCatService)
        {
            _FAQService = faqService;
            _FAQCatService = faqCatService;
        }

        public void OnGet()
        {
            FAQlist = _FAQService.GetAll();
            FAQCatlist = _FAQCatService.GetAll();
            FAQlist_sorted = FAQlist.OrderByDescending(x => x.ClickTime).ToList();

            if (FAQlist_sorted.Count > 5) {
                for (var i = 0; i < 5; i++)
                {
                    FAQlist_top5.Add(FAQlist_sorted[i]);
                }

                //foreach (var item in FAQlist_sorted) {
                //    FAQlist_top5.Add(item);
                //}
            }
            
        }

        public async void OnPostAddToCartAsync(string ID)
        {
            myFAQ = _FAQService.GetFAQById(ID);
            myFAQ.ClickTime += 1;
            _FAQService.UpdateFAQ(myFAQ);
            //test code
            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = string.Format("FAQ {0} click time is added", myFAQ.Question);
            FAQlist = _FAQService.GetAll();
        }

        //click time count function
        public void OnGetClickCount(string ID)
        {
            myFAQ = _FAQService.GetFAQById(ID);
            myFAQ.ClickTime += 1;
            _FAQService.UpdateFAQ(myFAQ);
            //test code
            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = string.Format("FAQ {0} click time is added", myFAQ.Question);
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

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                /*_FAQService.DeleteFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format(
                "FAQ{0} is Delete", myFAQ.ID);*/
            }
            return Page();

            //ID = ID;
            //Answer = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Answer) ? "null" : Answer);
            //Question = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Question) ? "null" : Question);
            //Creator = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Creator) ? "null" : Creator);
            //Editor = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Editor) ? "null" : Editor);
            //URL = string.Format("{0}",
            //string.IsNullOrWhiteSpace(URL) ? "null" : URL);
            //Date_Created = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Date_Created) ? "null" : Date_Created);


            return Page();
        }
    }


}

